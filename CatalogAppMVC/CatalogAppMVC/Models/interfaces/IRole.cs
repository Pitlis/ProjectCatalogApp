using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogAppMVC.Models.interfaces
{
    public interface IRole
    {

        string Id { get; set; }

        string Name { get; set; }

        long MaxSizeDownloadFileInternal { get; set; }

        long MaxSizeDownloadFileExternal { get; set; }

        long MaxSizeDownloadOnDay { get; set; }

        bool CanDownloadFile { get; set; }

        //TODO: interface IRole Проверить абстракцию типа коллекции.
        ICollection<CatalogAppMVC.Models.LinqToSqlMdl.CatalogCategories> AccessibleCategories { get; }

        //TODO: interface IRole Установка бонуса в МБ за спасибо.

    }
}