using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models.Identity
{
    public class CustomRole : IdentityRole, CatalogAppMVC.Models.interfaces.IRole
    {

        public CustomRole() { }

        public CustomRole(string name) { Name = name; }

        public long MaxSizeDownloadFileInternal
        {
            get;
            set;
        }

        public long MaxSizeDownloadFileExternal
        {
            get;
            set;
        }

        public long MaxSizeDownloadOnDay
        {
            get;
            set;
        }

        public bool CanDownloadFile
        {
            get;
            set;
        }

        public ICollection<LinqToSqlMdl.CatalogCategories> AccessibleCategories
        {
            //TODO: CustomRole сделать имплиментацию AccessibleCategories.
            get { throw new NotImplementedException(); }
        }

        public long SizeBonus
        {
            get;
            set;
        }
    }
}