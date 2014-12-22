using CatalogAppMVC.Models.LinqToSqlMdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models.Repositories
{
     partial class SqlRepositoryMain
    {


        public IQueryable<Specifications> Specifications
        {
            get
            {
                return Db.Specifications;
            }
        }

        public bool CreateSpecifications(Specifications instance)
        {
            if (instance.Id == 0)
            {
                Db.Specifications.InsertOnSubmit(instance);
                Db.Specifications.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateSpecifications(Specifications instance)
        {
            Specifications cache = Db.Specifications.Where(p => p.Id == instance.Id).FirstOrDefault();
            if (cache != null)
            {
                //TODO : Update fields for Specifications
                Db.Specifications.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveSpecifications(int idSpecifications)
        {
            Specifications instance = Db.Specifications.Where(p => p.Id == idSpecifications).FirstOrDefault();
            if (instance != null)
            {
                Db.Specifications.DeleteOnSubmit(instance);
                Db.Specifications.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}