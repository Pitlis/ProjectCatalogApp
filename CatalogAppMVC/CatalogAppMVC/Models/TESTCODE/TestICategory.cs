using CatalogAppMVC.Models.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models.TESTCODE
{
    public class TestICategory: ICategory
    {
        public List<Category> GetCategoriesForWrite(IUser user)
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
    }
}