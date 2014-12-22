using CatalogAppMVC.Models;
using CatalogAppMVC.Models.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using CatalogAppMVC.Models.TESTCODE;
using Ninject;
using CatalogAppMVC.Models.LinqToSqlMdl;


namespace CatalogAppMVC.Controllers
{
    public class RecordsController : Controller
    {
        IUser user;//активный пользователь
        ICategory categories = new TestICategory();
        ISpecification specification = new TestISpecification();

        [Inject]
        public IRepository repo { get; set; }

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
             
            ViewBag.Categories = new SelectList(repo.CatalogCategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddRecordCategory(Machinery someMachine)
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
           
            return View("Добавлено");
        }
    }
}