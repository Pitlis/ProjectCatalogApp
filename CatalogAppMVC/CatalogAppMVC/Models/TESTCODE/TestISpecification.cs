using CatalogAppMVC.Models.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models.TESTCODE
{
    public class TestISpecification
    {

        public static List<Specification> GetMandatSpecifications(int categoryID)
        {
            Specification cat1 = new Specification() { Name = "Вес", Value = "1000"};
            Specification cat2 = new Specification() { Name = "Ширина", Value = "61"};
            Specification cat3 = new Specification() { Name = "Высота", Value = "59"};
            List<Specification> list = new List<Specification>();
            list.Add(cat1);
            list.Add(cat2);
            list.Add(cat3);
            return list;
        }
    }
}