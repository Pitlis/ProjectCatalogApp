using CatalogAppMVC.Models.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models
{
    public class AccessRoleCategory
    {
        public int RoleID { get; set; }
        public int CategoryID { get; set; }
        public bool CanRead { get;  set; }
        public bool CanWrite { get;  set; }
        public bool CanDownloadFile { get;  set; }

        public AccessRoleCategory(int roleID, int categoryID, bool read, bool write, bool file)
        {
            RoleID = roleID;
            CategoryID = categoryID;
            CanRead = read;
            CanWrite = write;
            CanDownloadFile = file;
        }

        public AccessRoleCategory()
        {
            ;
        }

        public string GetNameRole
        {
            get
            {
                IRepository repository = new Repository();
                string name = "";
                try
                {
                    name = (from role in repository.Roles where role.Id == RoleID select role).Single().Name;
                }
                catch
                {
                    return RoleID.ToString();
                }
                return name;
            }

        }
        public string GetNameCategory
        {
            get
            {
                IRepository repository = new Repository();
                string name = "";
                try
                {
                    name = (from cat in repository.CatalogCategories where cat.Id == CategoryID select cat).Single().Name;
                }
                catch
                {
                    return CategoryID.ToString();
                }
                return name;
            }
        }




        //Методы для работы с БД

        public static List<AccessRoleCategory> GetAllAccess()
        {
            List<AccessRoleCategory> list = new List<AccessRoleCategory>();
            IRepository repository = new Repository();
            var accessFromBase = from acc in repository.Access select acc;
            foreach (var acc in accessFromBase)
            {
                list.Add(repository.ToAccess(acc));
            }
            return list;
        }
        public static void DeleteAccess(int categoryID, int roleID)
        {
            if (roleID != Access.ADMINID)
            {
                IRepository repository = new Repository();
                repository.RemoveAccess(GetAccess(categoryID, roleID));
            }
        }
        public static AccessRoleCategory GetAccess(int categoryID, int roleID)
        {
            IRepository repository = new Repository();
            try
            {
                var access = (from acc in repository.Access where (acc.CategoryID == categoryID) && (acc.RoleID == roleID) select acc).Single();
                return repository.ToAccess(access);
            }
            catch
            {
                return null;
            }
        }
        public static void EditAccess(AccessRoleCategory accessNew)
        {
            IRepository repository = new Repository();
            try
            {
                repository.UpdateAccess(accessNew);
            }
            catch { }
        }

        public static List<RoleView> GetAllRoles()
        {
            List<RoleView> list = new List<RoleView>();
            IRepository repository = new Repository();
            var roles = from role in repository.Roles select role;
            foreach (var role in roles)
            {
                list.Add(new RoleView() {ID = role.Id, Name = role.Name});
            }
            return list;
        }

        public void AddToBase()
        {
            IRepository repository = new Repository();
            try
            {
                repository.CreateAccess(this);
            }
            catch { }
        }
    }
}