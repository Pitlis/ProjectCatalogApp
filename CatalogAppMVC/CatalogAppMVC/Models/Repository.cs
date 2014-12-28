﻿using CatalogAppMVC.Models.interfaces;
using CatalogAppMVC.Models.WorkLinqToSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models
{
    public class Repository:IRepository
    {
        public IQueryable<LinqToSqlMdl.CatalogCategories> CatalogCategories
        {
            get { throw new NotImplementedException(); }
        }

        public bool CreateCatalogCategories(LinqToSqlMdl.CatalogCategories instance)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCatalogCategories(LinqToSqlMdl.CatalogCategories instance)
        {
            throw new NotImplementedException();
        }

        public bool RemoveCatalogCategories(int idCatalogCategories)
        {
            throw new NotImplementedException();
        }






        




        public IQueryable<LinqToSqlMdl.Machinery> Machinerys
        {
            get { throw new NotImplementedException(); }
        }
        
        public bool CreateMachinery(Record record)
        {
            try
            {
                CatalogDatabaseDataContext context = new WorkLinqToSql.CatalogDatabaseDataContext();
                Machinery machinery = new Machinery();

                machinery.title = record.Name;
                machinery.Description = record.Description;
                machinery.Status = (int)Record.Status.PREMODERATION;
                machinery.Category = 2;
                machinery.UserAuthor = 1;
                context.Machineries.InsertOnSubmit(machinery);
                context.Machineries.Context.SubmitChanges();

                foreach (Specification sp in record.Specifications)
                {
                    if (!CreateSpecifications(sp, machinery.Id))
                    {
                        throw new Exception("Невожно создать спецификацию");
                    }
                }
                if(record.Tags != null)
                {
                    foreach (Tag tag in record.Tags)
                    {
                        CreateTag(tag, machinery.Id);
                    }
                }

            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool UpdateMachinery(Record record)
        {
            throw new NotImplementedException();
        }

        public bool RemoveMachinery(int recordID)
        {
            throw new NotImplementedException();
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

        public bool UpdateSpecifications(Specification specification)
        {
            throw new NotImplementedException();
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






        public IQueryable<LinqToSqlMdl.Tags> Tags
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
                if(tagsInBase.Count() > 0)
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

        public bool UpdateTag(CatalogAppMVC.Models.Tag tag, int recordID)
        {
            throw new NotImplementedException();
        }

        public bool RemoveTag(int idTags)
        {
            try
            {
                CatalogDatabaseDataContext context = new WorkLinqToSql.CatalogDatabaseDataContext();

                WorkLinqToSql.Tag tag = (from t in context.Tags where t.Id == idTags select t).Single<WorkLinqToSql.Tag>();

                var machineTag = from mt in context.MachineTags where mt.TagID == tag.Id  select mt;

                foreach (WorkLinqToSql.MachineTag mt in machineTag)
                {
                    context.MachineTags.DeleteOnSubmit(mt);
                }
                context.MachineTags.Context.SubmitChanges();

                context.Tags.DeleteOnSubmit(tag);
                context.Tags.Context.SubmitChanges();

            }
            catch
            {
                return false;
            }

            return true;
        }


        public bool RemoveTag(int idTag, int recordID)
        {
            throw new NotImplementedException();
        }



        public IQueryable<Document> File
        {
            get { throw new NotImplementedException(); }
        }

        public bool CreateFile(Document tag)
        {
            throw new NotImplementedException();
        }

        public bool UpdateFile(Document tag)
        {
            throw new NotImplementedException();
        }

        public bool RemoveFile(int fileID)
        {
            throw new NotImplementedException();
        }
    }
}