using CatalogAppMVC.Models.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models
{
    public class Specification
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }




        //Методы для работы с БД

        public static List<Specification> GetMandatSpecifications(int categoryID)
        {
            IRepository repository = new Repository();
            List<Specification> list = new List<Specification>();
            try
            {
                var category = (from cat in repository.CatalogCategories where cat.Id == categoryID select cat).Single();
                var specifications = from sp in category.MandatSpecificCatalogCategories select sp.Specification;
                foreach(var sp in specifications)
                {
                    list.Add(repository.ToSpecification(sp));
                }
            }
            catch { }
            return list;
        }

        public static void DeleteMandatSpecification(int specificationID)
        {
            IRepository repository = new Repository();
            try
            {
                repository.RemoveMandatSpecification(specificationID);
            }
            catch { }
        }
        public static void CreateMandatSpecification(Specification sp, int categoryID)
        {
            IRepository repository = new Repository();
            try
            {
                repository.CreateMandatSpecification(sp, categoryID);
            }
            catch { }
        }
    }
}