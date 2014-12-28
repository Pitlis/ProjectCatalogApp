using CatalogAppMVC.Models.LinqToSqlMdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogAppMVC.Models.interfaces
{
    public interface IRepository
    {      

        #region CatalogCategories

        IQueryable<CatalogCategories> CatalogCategories { get; }

        bool CreateCatalogCategories(CatalogCategories instance);

        bool UpdateCatalogCategories(CatalogCategories instance);

        bool RemoveCatalogCategories(int idCatalogCategories);

        #endregion 
        #region Machinery

        IQueryable<Machinery> Machinerys { get; }

        bool CreateMachinery(Record record);

        bool UpdateMachinery(Record record);

        bool RemoveMachinery(int recordID);

        #endregion 
        #region Specifications

        IQueryable<WorkLinqToSql.Specification> Specifications { get; }

        bool CreateSpecifications(CatalogAppMVC.Models.Specification instance, int recordID);

        bool UpdateSpecifications(CatalogAppMVC.Models.Specification instance);

        bool RemoveSpecifications(int idSpecifications);

        #endregion 
        #region Tags

        IQueryable<Tags> Tags { get; }

        bool CreateTag(CatalogAppMVC.Models.Tag tag, int recordID);

        bool UpdateTag(CatalogAppMVC.Models.Tag tag, int recordID);

        bool RemoveTag(int idTag, int recordID);

        #endregion
        #region Files

        IQueryable<WorkLinqToSql.Document> File { get; }

        bool CreateFile(WorkLinqToSql.Document tag);

        bool UpdateFile(WorkLinqToSql.Document tag);

        bool RemoveFile(int fileID);

        #endregion 
        
        
    }
}
