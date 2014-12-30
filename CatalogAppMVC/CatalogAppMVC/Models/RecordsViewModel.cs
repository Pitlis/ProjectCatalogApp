using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models
{
    public class RecordsViewModel
    {
        public List<Record> records;
        public int categoryID { get; set; }

        public List<RecordIdBool> RecordsForView { get; set; }
        

        public class RecordIdBool { public bool check {get; set;} public int id {get; set;}}


    }
}