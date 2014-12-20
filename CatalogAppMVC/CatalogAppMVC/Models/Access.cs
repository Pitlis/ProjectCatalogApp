﻿using CatalogAppMVC.Models.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models
{
    public static class Access
    {
        public static bool CanDownloadFile(IUser user, int fileID)
        {
            //TODO: Разрешение на скачивание файла
            //заглушка!!
            return fileID == 1 ? true : false;
        }

        public static bool CanReadCategory(IUser user, int categoryID)
        {
            //TODO: Разрешение на просмотр категории (для защиты от подделки запроса)
            //заглушка!!
            return categoryID == 0 ? true : false;
        }
    }
}