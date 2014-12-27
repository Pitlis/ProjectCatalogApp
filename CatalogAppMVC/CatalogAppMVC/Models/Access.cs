using CatalogAppMVC.Models.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models
{
    public static class Access
    {
        public static bool CanDownloadFile(IMyAppAuthentication user, int fileID)
        {
            //TODO: Разрешение на скачивание файла
            //заглушка!!
            if (fileID == 1 || fileID == 7 || fileID == 8 || fileID == 9)
                return true;
            return false;
        }

        public static bool CanReadCategory(IMyAppAuthentication user, int categoryID)
        {
            //TODO: Разрешение на просмотр категории (для защиты от подделки запроса)
            //заглушка!!
            return categoryID == 0 ? true : false;
        }
    }
}