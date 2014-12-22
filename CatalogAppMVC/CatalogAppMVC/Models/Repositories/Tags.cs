using CatalogAppMVC.Models.LinqToSqlMdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models.Repositories
{
    partial  class SqlRepositoryMain
    {


        public IQueryable<Tags> Tags
        {
            get
            {
                return Db.Tags;
            }
        }

        public bool CreateTags(Tags instance)
        {
            if (instance.Id == 0)
            {
                Db.Tags.InsertOnSubmit(instance);
                Db.Tags.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateTags(Tags instance)
        {
            Tags cache = Db.Tags.Where(p => p.Id == instance.Id).FirstOrDefault();
            if (cache != null)
            {
                //TODO : Update fields for Tags
                Db.Tags.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveTags(int idTags)
        {
            Tags instance = Db.Tags.Where(p => p.Id == idTags).FirstOrDefault();
            if (instance != null)
            {
                Db.Tags.DeleteOnSubmit(instance);
                Db.Tags.Context.SubmitChanges();
                return true;
            }

            return false;
        }
        
    }
}