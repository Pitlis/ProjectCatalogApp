using CatalogAppMVC.Models.LinqToSqlMdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models.Repositories
{
     partial class SqlRepositoryMain
    {
         public Record GetRecord(int recordID)
         {
             throw new NotImplementedException();
         }

        public IQueryable<Machinery> Machinerys
        {
            get
            {
                return Db.Machinery;
            }
        }

        public bool CreateMachinery(Record record)
        {
            if (record.ID == 0)
            {
                //Db.Machinery.InsertOnSubmit(record);
                Db.Machinery.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateMachinery(Record record)
        {
            Machinery cache = Db.Machinery.Where(p => p.Id == record.ID).FirstOrDefault();
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