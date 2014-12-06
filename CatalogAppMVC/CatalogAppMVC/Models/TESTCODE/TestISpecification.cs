using CatalogAppMVC.Models.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models.TESTCODE
{
    public class TestISpecification: ISpecification
    {

        public List<Specification> GetMandatSpecifications(int categoryID)
        {
            Specification cat1 = new Specification() { Name = "Вес" };
            Specification cat2 = new Specification() { Name = "Ширина"};
            Specification cat3 = new Specification() { Name = "Высота"};
            List<Specification> list = new List<Specification>();
            list.Add(cat1);
            list.Add(cat2);
            list.Add(cat3);
            return list;
        }
    }
}