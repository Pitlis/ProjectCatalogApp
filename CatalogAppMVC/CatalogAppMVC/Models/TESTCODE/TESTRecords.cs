using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogAppMVC.Models.TESTCODE
{
    public static class TESTRecords
    {
        public static List<Record> GetRecords()
        {
            List<Record> list = new List<Record>();
            TESTRepository tr = new TESTRepository();
            Record r;
            r = tr.GetRecord(1);
            r.Name = "Запись1";
            r.ID = 0;
            list.Add(r);
            r = tr.GetRecord(1);
            r.Name = "Запись2";
            r.ID = 2;
            list.Add(r);
            r = tr.GetRecord(1);
            r.Name = "Запись3";
            r.ID = 2;
            list.Add(r);
            r = tr.GetRecord(1);
            r.Name = "Запись4";
            r.ID = 3;
            list.Add(r);

            return list;
        }
    }
}