using CatalogAppMVC.Models.interfaces;
using CatalogAppMVC.Models.LinqToSqlMdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models.Repositories
{
   public partial class SqlRepositoryMain
    {
        public IQueryable<CatalogCategories> CatalogCategories
        {
            get
            {
                return Db.CatalogCategories;
            }
        }

        public bool CreateCatalogCategories(CatalogCategories instance)
        {
            if (instance.Id == 0)
            {
                Db.CatalogCategories.InsertOnSubmit(instance);
                Db.CatalogCategories.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateCatalogCategories(CatalogCategories instance)
        {
            CatalogCategories cache = Db.CatalogCategories.Where(p => p.Id == instance.Id).FirstOrDefault();
            if (cache != null)
            {
                //TODO : Update fields for CatalogCategories
                Db.CatalogCategories.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveCatalogCategories(int idCatalogCategories)
        {
            CatalogCategories instance = Db.CatalogCategories.Where(p => p.Id == idCatalogCategories).FirstOrDefault();
            if (instance != null)
            {
                Db.CatalogCategories.DeleteOnSubmit(instance);
                Db.CatalogCategories.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}