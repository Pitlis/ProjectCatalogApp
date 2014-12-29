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
                //WorkLinqToSql.AspNetRole = (from r in repository.)

                string w = "213";
            }
            catch { }
            return list;
            
        }

        public static List<Category> GetOpenCategory(IMyAppAuthentication user)
        {
            throw new NotImplementedException();
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