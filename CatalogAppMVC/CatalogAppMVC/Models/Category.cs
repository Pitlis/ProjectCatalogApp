using CatalogAppMVC.Models.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }


        

        //Методы для работы с БД

        public static List<Category> GetCategoriesForWrite(IMyAppAuthentication user)
        {
            List<Category> list = new List<Category>();
            try
            {
                IRepository repository = new Repository();
                int userID = user.GetAuthenticationUserId();
                int userRoleId = repository.GetUserRole(userID).Id;

                IQueryable<WorkLinqToSql.AccessCatalogCategory> access = from acc in repository.Access where (acc.RoleID == userRoleId) && (acc.W == true) select acc;
                foreach(WorkLinqToSql.AccessCatalogCategory acc in access)
                {
                    list.Add(repository.ToCategory(acc.CatalogCategory));
                }
            }
            catch { }
            return list;
        }

        public static List<Category> GetOpenCategory(IMyAppAuthentication user)
        {
            List<Category> list = new List<Category>();
            try
            {
                IRepository repository = new Repository();
                int userID = user.GetAuthenticationUserId();
                int userRoleId = repository.GetUserRole(userID).Id;

                IQueryable<WorkLinqToSql.AccessCatalogCategory> access = from acc in repository.Access where (acc.RoleID == userRoleId) && (acc.R == true) select acc;
                foreach (WorkLinqToSql.AccessCatalogCategory acc in access)
                {
                    list.Add(repository.ToCategory(acc.CatalogCategory));
                }
            }
            catch { }
            return list;
        }
        public static List<Category> GetAllCategory()
        {
            IRepository repository = new Repository();
            List<Category> list = new List<Category>();
            var categoriesFromBase = from cat in repository.CatalogCategories select cat;
            foreach(WorkLinqToSql.CatalogCategory c in categoriesFromBase)
            {
                list.Add(repository.ToCategory(c));
            }
            return list;
        }
    }
}