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
            Record r;
            r = GetRecord(1);
            r.Name = "Запись1";
            r.ID = 0;
            list.Add(r);
            r = GetRecord(1);
            r.Name = "Запись2";
            r.ID = 2;
            list.Add(r);
            r = GetRecord(1);
            r.Name = "Запись3";
            r.ID = 2;
            list.Add(r);
            r = GetRecord(1);
            r.Name = "Запись4";
            r.ID = 3;
            list.Add(r);

            return list;
        }

        public static CatalogAppMVC.Models.Record GetRecord(int RecordID)
        {
            //TODO добавить в класс репозитория метод получения записи
            //Все ниже - заглушка!!!
            Record record = new Record() { ID = 1, Name = "Test Record", Description = "Помните такой телефон — Nokia 3310? Разумеется, помните! А такую штуку как синтезатор мелодий в нем? Тоже помните, отлично. А по старым, теплым и ламповым мелодиям скучаете? Вот и я скучаю. А еще мне на глаза попался сайтик с более чем сотней нотных листов для этого редактора. И что я должен был оставить эту прелесть без внимания? Нет уж. Что я сделал? Правильно! Взял и написал точно такой же генератор мелодий, который позволяет на выходе получить" };
            record.Specifications = TestISpecification.GetMandatSpecifications(1);
            record.Files = TESTDocuments.GetFilesTEST();
            record.Tags = Tag.CreateTagsFromString("test1, test2, test3, test4");
            record.Files = TESTDocuments.GetFilesTEST();
            return record;
        }
    }
}