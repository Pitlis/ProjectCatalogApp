using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogAppMVC.Models.interfaces
{
    public interface IRepository
    {
        WorkLinqToSql.AspNetRole GetUserRole(int userID);
        IQueryable<WorkLinqToSql.AspNetUser> Users { get; }

        #region CatalogCategories

        IQueryable<WorkLinqToSql.CatalogCategory> CatalogCategories { get; }

        int CreateCatalogCategories(Category categoryModel);

        bool UpdateCatalogCategories(Category categoryModel);

        bool RemoveCatalogCategories(int idCatalogCategories);

        #endregion 
        #region Machinery

        IQueryable<WorkLinqToSql.Machinery> Machinerys { get; }

        int CreateMachinery(Record record);

        bool UpdateMachinery(Record record);

        bool RemoveMachinery(int recordID);

        bool UpdateStatusMachinery(Record.StatusType statusNew, int recordID);

        #endregion 
        #region Specifications

        IQueryable<WorkLinqToSql.Specification> Specifications { get; }

        bool CreateSpecifications(CatalogAppMVC.Models.Specification instance, int recordID);

        bool UpdateSpecifications(CatalogAppMVC.Models.Specification instance);

        bool RemoveSpecifications(int idSpecifications);

        #endregion 
        #region Tags

        IQueryable<WorkLinqToSql.Tag> Tags { get; }

        bool CreateTag(CatalogAppMVC.Models.Tag tag, int recordID);

        bool UpdateTag(CatalogAppMVC.Models.Tag tag);

        bool RemoveTag(int idTag, int recordID);

        #endregion
        #region Files

        IQueryable<WorkLinqToSql.Document> File { get; }

        bool CreateFile(File file);

        bool UpdateFile(File file);

        bool RemoveFile(int fileID);

        #endregion 
        #region Access

        IQueryable<WorkLinqToSql.AccessCatalogCategories> Access { get; }
        bool CreateAccess(AccessRoleCategory accessModel);
        bool UpdateAccess(AccessRoleCategory accessModel);
        bool RemoveAccess(AccessRoleCategory accessModel);

        #endregion

        #region Convert

        Record ToRecord(WorkLinqToSql.Machinery machinery);
        Category ToCategory(WorkLinqToSql.CatalogCategory catalogCategory);
        Specification ToSpecification(WorkLinqToSql.Specification specificationFromBase);
        Tag ToTag(WorkLinqToSql.Tag tagFromBase);
        File ToFile(WorkLinqToSql.Document document);

        #endregion
    }
}
