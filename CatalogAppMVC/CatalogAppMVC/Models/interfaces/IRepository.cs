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
<<<<<<< HEAD
       

        #region CatalogCategories

        IQueryable<CatalogCategories> CatalogCategories { get; }

        bool CreateCatalogCategories(CatalogCategories instance);

        bool UpdateCatalogCategories(CatalogCategories instance);

        bool RemoveCatalogCategories(int idCatalogCategories);

        #endregion 
        #region Machinery

        IQueryable<Machinery> Machinerys { get; }

        bool CreateMachinery(Machinery instance);

        bool UpdateMachinery(Machinery instance);

        bool RemoveMachinery(int idMachinery);

        #endregion 
        #region Specifications

        IQueryable<Specifications> Specifications { get; }

        bool CreateSpecifications(Specifications instance);

        bool UpdateSpecifications(Specifications instance);

        bool RemoveSpecifications(int idSpecifications);

        #endregion 
        #region Tags

        IQueryable<Tags> Tags { get; }

        bool CreateTags(Tags instance);

        bool UpdateTags(Tags instance);

        bool RemoveTags(int idTags);

        #endregion 
        
        
        
        
=======
        bool CreateRecord(Record record);
        CatalogAppMVC.Models.Record GetRecord(int RecordID);
        CatalogAppMVC.Models.File GetFile(int fileID);

>>>>>>> a7292b63813708bece0642ba686214e122732f97
    }
}
