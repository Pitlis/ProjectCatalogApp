using CatalogAppMVC.Models.interfaces;
using CatalogAppMVC.Models.WorkLinqToSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models
{
    public class Repository : IRepository
    {
        public IQueryable<WorkLinqToSql.CatalogCategory> CatalogCategories
        {
            get { return new CatalogDatabaseDataContext().CatalogCategories; }
        }

        public int CreateCatalogCategories(Category categoryModel)
        {
            WorkLinqToSql.CatalogCategory category = new CatalogCategory();
            try
            {
                CatalogDatabaseDataContext context = new CatalogDatabaseDataContext();

                var categoryNames = from cat in context.CatalogCategories select cat.Name;
                if (!categoryNames.Contains(categoryModel.Name))
                {
                    category.Name = categoryModel.Name;
                    context.CatalogCategories.InsertOnSubmit(category);
                    context.CatalogCategories.Context.SubmitChanges();
                }
            }
            catch
            {
                return -1;
            }
            return category.Id;
        }

        public bool UpdateCatalogCategories(Category categoryModel)
        {
            try
            {
                CatalogDatabaseDataContext context = new WorkLinqToSql.CatalogDatabaseDataContext();

                var categoryNames = from cat in context.CatalogCategories select cat.Name;
                if (!categoryNames.Contains(categoryModel.Name))
                {
                    WorkLinqToSql.CatalogCategory category = (from c in context.CatalogCategories where c.Id == categoryModel.ID select c).Single<WorkLinqToSql.CatalogCategory>();
                    category.Name = categoryModel.Name;
                    context.CatalogCategories.Context.SubmitChanges();
                }


            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool RemoveCatalogCategories(int idCatalogCategories)
        {
            try
            {
                CatalogDatabaseDataContext context = new WorkLinqToSql.CatalogDatabaseDataContext();

                WorkLinqToSql.CatalogCategory category = (from c in context.CatalogCategories where c.Id == idCatalogCategories select c).Single<WorkLinqToSql.CatalogCategory>();

                if (category.Name == "Others")
                    return false;

                var mandatSpecifications = from ms in context.MandatSpecificCatalogCategories where ms.CatalogCategoryID == category.Id select ms;
                foreach(var ms in mandatSpecifications)
                {
                    context.MandatSpecificCatalogCategories.DeleteOnSubmit(ms);
                }
                context.MandatSpecificCatalogCategories.Context.SubmitChanges();

                var otherCategory = from c in context.CatalogCategories where c.Name == "Others" select c;
                int otherCategoryID = 0;
                if (otherCategory.Count() == 0)
                {
                    otherCategoryID = CreateCatalogCategories(new Category() { Name = "Others" });
                }
                else
                {
                    otherCategoryID = otherCategory.Single<CatalogCategory>().Id;
                }

                var machineries = from machinery in category.Machineries select machinery;
                foreach(var machinery in machineries)
                {
                    machinery.Category = otherCategoryID;
                }

                context.CatalogCategories.DeleteOnSubmit(category);
                context.CatalogCategories.Context.SubmitChanges();

            }
            catch
            {
                return false;
            }
            return true;
        }



        public IQueryable<WorkLinqToSql.Machinery> Machinerys
        {
            get { return new CatalogDatabaseDataContext().Machineries; }
        }

        public int CreateMachinery(Record record)
        {
            Machinery machinery = new Machinery();
            try
            {
                CatalogDatabaseDataContext context = new WorkLinqToSql.CatalogDatabaseDataContext();

                machinery.title = record.Name;
                machinery.Description = record.Description;
                machinery.Status = (int)Record.StatusType.PREMODERATION;
                machinery.Category = record.CategoryID;
                machinery.UserAuthor = record.UserAuthorID;
                context.Machineries.InsertOnSubmit(machinery);
                context.Machineries.Context.SubmitChanges();

                foreach (Specification sp in record.Specifications)
                {
                    if (!CreateSpecifications(sp, machinery.Id))
                    {
                        throw new Exception("Невожно создать спецификацию");
                    }
                }
                if (record.Tags != null)
                {
                    foreach (Tag tag in record.Tags)
                    {
                        CreateTag(tag, machinery.Id);
                    }
                }

            }
            catch
            {
                return 0;
            }
            return machinery.Id;
        }

        public bool UpdateMachinery(Record record)
        {
            try
            {
                CatalogDatabaseDataContext context = new WorkLinqToSql.CatalogDatabaseDataContext();
                WorkLinqToSql.Machinery machinery = (from m in context.Machineries where m.Id == record.ID select m).Single<WorkLinqToSql.Machinery>();
                
                machinery.Description = record.Description;
                machinery.Status = (int)record.Status;
                machinery.Category = record.CategoryID;
                machinery.UserAuthor = record.UserAuthorID;
                context.Machineries.Context.SubmitChanges();

                foreach (Specification sp in record.Specifications)
                {
                    if (!UpdateSpecifications(sp))
                    {
                        throw new Exception("Ошибка обновления спецификации");
                    }
                }
                if (record.Tags != null)
                {
                    foreach (Tag tag in record.Tags)
                    {
                        UpdateTag(tag);
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool RemoveMachinery(int recordID)
        {
            try
            {
                CatalogDatabaseDataContext context = new WorkLinqToSql.CatalogDatabaseDataContext();
                WorkLinqToSql.Machinery machinery = (from m in context.Machineries where m.Id == recordID select m).Single<WorkLinqToSql.Machinery>();

                var specificationsID = from s in context.MachineSpecifications where s.MachineID == machinery.Id select s.SpecificationID;
                if (specificationsID.Count() > 0)
                {
                    foreach (int spID in specificationsID)
                    {
                        RemoveSpecifications(spID);
                    }
                }
                var tagsID = from t in context.MachineTags where t.MachineID == machinery.Id select t.TagID;
                if (tagsID.Count() > 0)
                {
                    foreach (int tagID in tagsID)
                    {
                        RemoveTag(tagID, machinery.Id);
                    }
                }
                var filesID = from f in context.Document where f.MachineID == machinery.Id select f.Id;
                if(filesID.Count() > 0)
                {
                    foreach(int fileID in filesID)
                    {
                        RemoveFile(fileID);
                    }
                }

                context.Machineries.DeleteOnSubmit(machinery);
                context.Machineries.Context.SubmitChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool UpdateStatusMachinery(Record.StatusType statusNew, int recordID)
        {
            try
            {
                WorkLinqToSql.CatalogDatabaseDataContext context = new CatalogDatabaseDataContext();
                WorkLinqToSql.Machinery machinery = (from m in context.Machineries where m.Id == recordID select m).Single<WorkLinqToSql.Machinery>();
                machinery.Status = (int)statusNew;
                context.Machineries.Context.SubmitChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }


        public IQueryable<WorkLinqToSql.Specification> Specifications
        {
            get { throw new NotImplementedException(); }
        }

        public bool CreateSpecifications(Specification specificationModel, int recordID)
        {
            try
            {
                CatalogDatabaseDataContext context = new WorkLinqToSql.CatalogDatabaseDataContext();
                WorkLinqToSql.Specification specification = new WorkLinqToSql.Specification();
                WorkLinqToSql.MachineSpecification machinespecification = new MachineSpecification();

                specification.Name = specificationModel.Name;
                specification.Value = specificationModel.Value;

                machinespecification.MachineID = recordID;
                machinespecification.Specification = specification;

                context.Specifications.InsertOnSubmit(specification);
                context.MachineSpecifications.InsertOnSubmit(machinespecification);


                context.Specifications.Context.SubmitChanges();
                context.MachineSpecifications.Context.SubmitChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool UpdateSpecifications(Specification specificationModel)
        {
            try
            {
                CatalogDatabaseDataContext context = new WorkLinqToSql.CatalogDatabaseDataContext();

                WorkLinqToSql.Specification specification = (from s in context.Specifications where s.Id == specificationModel.ID select s).Single<WorkLinqToSql.Specification>();
                specification.Name = specificationModel.Name;
                specification.Value = specificationModel.Value;

                context.Specifications.Context.SubmitChanges();

            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool RemoveSpecifications(int idSpecifications)
        {
            try
            {
                CatalogDatabaseDataContext context = new WorkLinqToSql.CatalogDatabaseDataContext();

                WorkLinqToSql.Specification specification = (from s in context.Specifications where s.Id == idSpecifications select s).Single<WorkLinqToSql.Specification>();

                var macnineSpecifications = from ms in context.MachineSpecifications where ms.SpecificationID == specification.Id select ms;

                foreach (WorkLinqToSql.MachineSpecification ms in macnineSpecifications)
                {
                    context.MachineSpecifications.DeleteOnSubmit(ms);
                }
                context.MachineSpecifications.Context.SubmitChanges();

                context.Specifications.DeleteOnSubmit(specification);
                context.Specifications.Context.SubmitChanges();

            }
            catch
            {
                return false;
            }

            return true;
        }



        public IQueryable<WorkLinqToSql.Tag> Tags
        {
            get { throw new NotImplementedException(); }
        }

        public bool CreateTag(CatalogAppMVC.Models.Tag tagModel, int recordID)
        {
            try
            {
                CatalogDatabaseDataContext context = new WorkLinqToSql.CatalogDatabaseDataContext();
                WorkLinqToSql.Tag tag = new WorkLinqToSql.Tag();
                tag.Name = tagModel.Name;

                var tagsInBase = from t in context.Tags where t.Name == tagModel.Name select t;
                if (tagsInBase.Count() > 0)
                {
                    tag = tagsInBase.First();
                }
                else
                {
                    context.Tags.InsertOnSubmit(tag);
                    context.Tags.Context.SubmitChanges();
                }

                WorkLinqToSql.MachineTag machineTag = new MachineTag();

                machineTag.MachineID = recordID;
                machineTag.Tag = tag;

                context.MachineTags.InsertOnSubmit(machineTag);
                context.MachineTags.Context.SubmitChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool UpdateTag(CatalogAppMVC.Models.Tag tagModel)
        {
            try
            {
                CatalogDatabaseDataContext context = new WorkLinqToSql.CatalogDatabaseDataContext();
                WorkLinqToSql.Tag tag = (from t in context.Tags where t.Id == tagModel.ID select t).Single<WorkLinqToSql.Tag>();

                tag.Name = tagModel.Name;
                context.MachineTags.Context.SubmitChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool RemoveTag(int idTags, int recordID)
        {
            try
            {
                CatalogDatabaseDataContext context = new WorkLinqToSql.CatalogDatabaseDataContext();

                WorkLinqToSql.Tag tag = (from t in context.Tags where t.Id == idTags select t).Single<WorkLinqToSql.Tag>();

                var machineTag = from mt in context.MachineTags where (mt.TagID == tag.Id) && (mt.MachineID == recordID) select mt;

                foreach (WorkLinqToSql.MachineTag mt in machineTag)
                {
                    context.MachineTags.DeleteOnSubmit(mt);
                }
                context.MachineTags.Context.SubmitChanges();

                var otherTagMachine = from mt in context.MachineTags where mt.TagID == tag.Id select mt;
                if (otherTagMachine.Count() == 0)
                {
                    context.Tags.DeleteOnSubmit(tag);
                    context.Tags.Context.SubmitChanges();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }



        public IQueryable<Document> File
        {
            get { throw new NotImplementedException(); }
        }

        public bool CreateFile(File file)
        {
            try
            {
                CatalogDatabaseDataContext context = new WorkLinqToSql.CatalogDatabaseDataContext();
                WorkLinqToSql.Document document = new WorkLinqToSql.Document();
                document.UserAuthor = file.AuthorName;
                document.Status = (int)Record.StatusType.PREMODERATION;
                document.MachineID = file.RecordID;
                document.PathToFile = file.PachToFile;
                document.DocumentName = file.DocumentName;
                document.DocumentType = file.DocumentType;
                document.FileName = file.FileName;
                document.FileType = file.FileType;
                document.Size = file.Size;
                document.MachineID = file.RecordID;

                context.Document.InsertOnSubmit(document);
                context.Document.Context.SubmitChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool UpdateFile(File file)
        {
            throw new NotImplementedException();
        }

        public bool RemoveFile(int fileID)
        {
            try
            {
                CatalogDatabaseDataContext context = new CatalogDatabaseDataContext();
                WorkLinqToSql.Document doc = (from file in context.Document where file.Id == fileID select file).Single<WorkLinqToSql.Document>();
                context.Document.DeleteOnSubmit(doc);
                context.Document.Context.SubmitChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public IQueryable<WorkLinqToSql.AccessCatalogCategory> Access
        {
            get
            {
                return new CatalogDatabaseDataContext().AccessCatalogCategories;
            }
        }

        public bool CreateAccess(AccessRoleCategory accessModel)
        {
            CatalogDatabaseDataContext context = new CatalogDatabaseDataContext();
            try
            {
                WorkLinqToSql.AccessCatalogCategory access = new AccessCatalogCategory();
                access.F = accessModel.CanDownloadFile;
                access.R = accessModel.CanRead;
                access.W = accessModel.CanWrite;
                access.RoleID = accessModel.RoleID;
                access.CategoryID = accessModel.CategoryID;
                context.AccessCatalogCategories.InsertOnSubmit(access);
                context.AccessCatalogCategories.Context.SubmitChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool UpdateAccess(AccessRoleCategory accessModel);

        public bool RemoveAccess(AccessRoleCategory accessModel);

#region Convert

        public Record ToRecord(WorkLinqToSql.Machinery machinery)
        {
            Record record = new Record();
            record.SetID(machinery.Id);
            record.Name = machinery.title;
            record.Description = machinery.Description;
            record.UserAuthorID = machinery.UserAuthor;
            record.CategoryID = machinery.Category;
            record.ChangeStatus((Record.StatusType)machinery.Status);

            record.Specifications = new List<Specification>();
            foreach(WorkLinqToSql.MachineSpecification mSP in machinery.MachineSpecifications)
            {
                record.Specifications.Add(ToSpecification(mSP.Specification));
            }

            if (machinery.MachineTags.Count() > 0)
            {
                record.Tags = new List<Tag>();
                foreach(WorkLinqToSql.MachineTag mT in machinery.MachineTags)
                {
                    record.Tags.Add(ToTag(mT.Tag));
                }
            }
            else
            {
                record.Tags = null;
            }

            if(machinery.Document.Count() > 0)
            {
                record.Files = new List<File>();
                foreach(WorkLinqToSql.Document document in machinery.Document)
                {
                    record.Files.Add(ToFile(document));
                }
            }
            else
            {
                record.Files = null;
            }


            return record;
        }

        public Category ToCategory(WorkLinqToSql.CatalogCategory catalogCategory)
        {
            Category category = new Category();
            category.ID = catalogCategory.Id;
            category.Name = catalogCategory.Name;
            return category;
        }

        public Specification ToSpecification(WorkLinqToSql.Specification specificationFromBase)
        {
            Specification specification = new Specification();
            specification.ID = specificationFromBase.Id;
            specification.Name = specificationFromBase.Name;
            specification.Value = specificationFromBase.Value;
            return specification;
        }

        public Tag ToTag(WorkLinqToSql.Tag tagFromBase)
        {
            Tag tag = new Tag();
            tag.ID = tagFromBase.Id;
            tag.Name = tagFromBase.Name;
            return tag;
        }

        public File ToFile(WorkLinqToSql.Document document)
        {
            throw new NotImplementedException();
        }
#endregion





    }
}