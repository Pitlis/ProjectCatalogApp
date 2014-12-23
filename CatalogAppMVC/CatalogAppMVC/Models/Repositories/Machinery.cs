using CatalogAppMVC.Models.LinqToSqlMdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models.Repositories
{
     partial class SqlRepositoryMain
    {


        public IQueryable<Machinery> Machinerys
        {
            get
            {
                return Db.Machinery;
            }
        }

        public bool CreateMachinery(Machinery instance)
        {
            if (instance.Id == 0)
            {
                Db.Machinery.InsertOnSubmit(instance);
                Db.Machinery.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateMachinery(Machinery instance)
        {
            Machinery cache = Db.Machinery.Where(p => p.Id == instance.Id).FirstOrDefault();
            if (cache != null)
            {
                //TODO : Update fields for Machinery
                Db.Machinery.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveMachinery(int idMachinery)
        {
            Machinery instance = Db.Machinery.Where(p => p.Id == idMachinery).FirstOrDefault();
            if (instance != null)
            {
                Db.Machinery.DeleteOnSubmit(instance);
                Db.Machinery.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}