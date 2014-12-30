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
        public const int GUESTID = 2;
        public const int ADMINID = 1;

        public static bool CanDownloadFile(int userID, int fileID)
        {
            try
            {
                IRepository repository = new Repository();
                int roleID = repository.GetUserRole(userID).Id;
                var file = (from f in repository.File where f.Id == fileID select f).Single();
                int categoryID = file.Machinery.CatalogCategory.Id;

                var access = (from acc in repository.Access where (acc.RoleID == roleID) && (acc.CategoryID == categoryID) select acc).Single();
                if (access.F)
                    return true;
            }
            catch { }
            return false;
        }

        public static bool CanReadCategory(int userID, int categoryID)
        {
            IRepository repository = new Repository();
            bool canRead = false;
            try
            {
                int roleID = repository.GetUserRole(userID).Id;
                var access = (from acc in repository.Access where (acc.CategoryID == categoryID) && (acc.RoleID == roleID) select acc).Single();
                if (access.R)
                {
                    canRead = true;
                }
            }
            catch
            {
                return false;
            }
            return canRead;
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