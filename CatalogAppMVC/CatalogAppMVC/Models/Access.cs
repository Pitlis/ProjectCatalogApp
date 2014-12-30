using CatalogAppMVC.Models.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CatalogAppMVC.Models.Identity;

namespace CatalogAppMVC.Models
{
    public static class Access
    {
        const int GUESTID = 2;

        public static bool CanDownloadFile(int userID, int fileID)
        {
            //TODO: Разрешение на скачивание файла
            //заглушка!!
            if (fileID == 1 || fileID == 7 || fileID == 8 || fileID == 9)
                return true;
            return false;
        }

        public static bool CanReadCategory(int userID, int categoryID)
        {
            //TODO: Разрешение на просмотр категории (для защиты от подделки запроса)
            //заглушка!!
            return categoryID == 0 ? true : false;
        }

        public static int GetUserID(System.Security.Principal.IPrincipal userSecurity, HttpContextBase httpContext)
        {
            int userID = GUESTID;
            if(userSecurity.Identity.IsAuthenticated)
            {
                IMyAppAuthentication user = new ApplicationAuthentication(httpContext);
                userID = user.GetAuthenticationUserId();
            }

            return userID;
        }
    }
}