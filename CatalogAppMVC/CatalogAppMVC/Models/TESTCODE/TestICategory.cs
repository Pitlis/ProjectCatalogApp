using CatalogAppMVC.Models.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models.TESTCODE
{
    public class TestICategory
    {
        public static List<Category> GetCategoriesForWrite(IUser user)
        {
            Category cat1 = new Category() { Name = "Категория 1", ID = 0 };
            Category cat2 = new Category() { Name = "Категория 2", ID = 1 };
            Category cat3 = new Category() { Name = "Категория 3", ID = 2 };
            List<Category> list = new List<Category>();
            list.Add(cat1);
            list.Add(cat2);
            list.Add(cat3);
            return list;
        }

        public static List<Category> GetOpenCategory(IUser user)
        {
            Category cat1 = new Category() { Name = "Категория 1", ID = 0 };
            Category cat2 = new Category() { Name = "Категория 2", ID = 1 };
            Category cat3 = new Category() { Name = "Категория 3", ID = 2 };
            Category cat4 = new Category() { Name = "Категория 4", ID = 3 };
            Category cat5 = new Category() { Name = "Категория 5", ID = 4 };
            Category cat6 = new Category() { Name = "Категория 6", ID = 5 };
            Category cat7 = new Category() { Name = "Категория 7", ID = 6 };
            List<Category> list = new List<Category>();
            list.Add(cat1);
            list.Add(cat2);
            list.Add(cat3);
            list.Add(cat4);
            list.Add(cat5);
            list.Add(cat6);
            list.Add(cat7);
            return list;
        }
    }
}