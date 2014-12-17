using CatalogAppMVC.Models;
using CatalogAppMVC.Models.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using CatalogAppMVC.Models.TESTCODE;


namespace CatalogAppMVC.Controllers
{
    public class RecordsController : Controller
    {
        IUser user;//активный пользователь
        ICategory categories = new TestICategory();
        ISpecification specification = new TestISpecification();
        IRepository repository;

        #region newRecord

        [HttpGet]
        public ActionResult AddRecord()
        {
            Record record = Session["Record"] as Record;
            return View(record);
        }

        [HttpPost]
        public ActionResult AddRecord(Record recordNew, string TagsString)
        {
            Record record = Session["Record"] as Record;
            record.LoadFromPage(recordNew);
            record.LoadTagsFromString(TagsString);

            Session["Record"] = record;
            return RedirectToAction("AddRecordFiles");
        }




        [HttpGet]
        public ActionResult AddRecordCategory()
        {
            SelectList categoriesList = new SelectList(categories.GetCategoriesForWrite(user), "ID", "Name");
            ViewBag.Categories = categoriesList;
            return View();
        }

        [HttpPost]
        public ActionResult AddRecordCategory(int CategorySelected)
        {
            //TODO Добавить проверку, правда ли пользователь выбрал одну из доступных ему категорий 
            Session["Record"] = new Record(user, CategorySelected, specification);
            return RedirectToAction("AddRecord");
        }




        [HttpGet]
        public ActionResult AddRecordFiles()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRecordFiles(object obl)
        {
            Record record = Session["Record"] as Record;
            repository.CreateRecord(record);
            return View("Добавлено");
        }


        #endregion

        Record record1 = new Record() { ID = 1, Name = "Test Record", Description = "Помните такой телефон — Nokia 3310? Разумеется, помните! А такую штуку как синтезатор мелодий в нем? Тоже помните, отлично. А по старым, теплым и ламповым мелодиям скучаете? Вот и я скучаю. А еще мне на глаза попался сайтик с более чем сотней нотных листов для этого редактора. И что я должен был оставить эту прелесть без внимания? Нет уж. Что я сделал? Правильно! Взял и написал точно такой же генератор мелодий, который позволяет на выходе получить" };
        public ActionResult RecordView(Record record)
        {
            record = record1;
            record.Specifications = specification.GetMandatSpecifications(1);
            record.Tags = Tag.CreateTagsFromString("test1, test2, test3, test4");


            return View(record);
        }


    }
}