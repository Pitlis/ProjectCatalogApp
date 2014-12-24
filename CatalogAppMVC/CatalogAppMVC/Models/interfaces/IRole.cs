using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogAppMVC.Models.interfaces
{
    interface IRole
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public long MaxSizeDownloadFileInternal { get; set; }

        public long MaxSizeDownloadFileExternal { get; set; }

        public long MaxSizeDownloadOnDay { get; set; }

        public bool CanDownloadFile { get; set; }

        //TODO: interface IRole Проверить абстракцию типа коллекции.
        public ICollection<CatalogAppMVC.Models.LinqToSqlMdl.CatalogCategories> AccessibleCategories();

        //TODO: interface IRole Установка бонуса в МБ за спасибо.

    }
}