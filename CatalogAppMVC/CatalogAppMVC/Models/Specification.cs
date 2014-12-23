using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models
{
    public class Specification
    {
        public string Name { get; set; }
        public string Value { get; set; }




        //Методы для работы с БД

        public static List<Specification> GetMandatSpecifications(int categoryID)
        {
            throw new NotImplementedException();
        }
    }
}