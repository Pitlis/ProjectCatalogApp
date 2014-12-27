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
            throw new NotImplementedException();
        }
        public static List<Category> GetOpenCategory(IMyAppAuthentication user)
        {
            throw new NotImplementedException();
        }

    }
}