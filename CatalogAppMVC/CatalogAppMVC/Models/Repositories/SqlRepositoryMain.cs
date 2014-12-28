using CatalogAppMVC.Models.interfaces;
using CatalogAppMVC.Models.LinqToSqlMdl;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models.Repositories
{
    public partial class SqlRepositoryMain
    {
        [Inject]
        public DataClassesDataContext Db { get; set; }
    }
}