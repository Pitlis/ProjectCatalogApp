using CatalogAppMVC.Models;
using CatalogAppMVC.Models.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using CatalogAppMVC.Models.TESTCODE;
using System.Configuration;
using Ninject;
using CatalogAppMVC.Models.LinqToSqlMdl;
using CatalogAppMVC.Models.Identity;
using System.Reflection;
using System.Threading;


namespace CatalogAppMVC.Controllers
{
    public class RecordsController : Controller
    {
        #region newRecord

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
            int userID = Access.GetUserID(User, HttpContext);
            ViewBag.Categories = new SelectList(Category.GetCategoriesForWrite(userID), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddRecordCategory(int CategorySelected)
        {
            int userID = Access.GetUserID(User, HttpContext);
            //TODO Добавить проверку, правда ли пользователь выбрал одну из доступных ему категорий
            Session["Record"] = new Record(userID, CategorySelected);
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


        #endregion

        #region OneRecordView

        public ActionResult RecordView(Record record)
        {
            record = TESTRecords.GetRecord(111);
            return View(record);
        }
        public ActionResult TryDownloadFile(int fileID)
        {
            int userID = Access.GetUserID(User, HttpContext);
            if (Access.CanDownloadFile(userID, fileID))
            {
                return RedirectToAction("DownloadFile", new { FileID = fileID });
            }
            return View("DownloadFileError");
        }

        public FileResult DownloadFile(int FileID)
        {
            CatalogAppMVC.Models.File file = TESTDocuments.GetFile(FileID);
            return File(file.GetPatchToFile(), CatalogAppMVC.Models.File.TYPEFILES, file.GetFileName());
        }


        #endregion

        public ActionResult TwoRecordsView(IEnumerable<Record> records)
        {
            //TODO Удалить заглушку
            List<Record> records1 = new List<Record>();
            records1.Add(TESTRecords.GetRecord(1));
            records1[0].Name = "Запись 1";
            records1.Add(TESTRecords.GetRecord(2));
            records1[1].Name = "Запись 2";
            records1[0].Specifications.Add(new Specification() { Name = "цвет1", Value = "зеленый" });
            records1[0].Specifications.Add(new Specification() { Name = "цвет2", Value = "синий" });
            records1[0].Specifications.Add(new Specification() { Name = "цвет3", Value = "желтый" });

            records1[1].Specifications.Add(new Specification() { Name = "цвет2", Value = "фиолетовый" });
            records1[1].Specifications.Add(new Specification() { Name = "цвет4", Value = "красный" });
            records1[1].Specifications.Add(new Specification() { Name = "цвет5", Value = "голубой" });
            records = records1;
            //---
            records1[0].Tags.Add(new Tag() { Name = "tag1" });
            records1[0].Tags.Add(new Tag() { Name = "tag2" });
            records1[0].Tags.Add(new Tag() { Name = "tag3" });
            records1[0].Tags.Add(new Tag() { Name = "tag2" });

            IRepository repository = new Repository();
            //Parser parser = new Parser();
            //parser.ParseSite(Assembly.LoadFrom("D:\\parser_infofrezer_ru.dll"));
            //for (int i = 60; i <= 65; i++)
                //repository.RemoveMachinery(i);
            //Category.GetCategoriesForWrite(AuthUser);
            //AccessRoleCategory access = new AccessRoleCategory(1, 11, true, true, true);
            //repository.CreateAccess(access);

            //Category.GetCategoriesForWrite(AuthUser);
            //(new Category() { Name = "Шпиндели и инверторы" }).AddToBase();
            //(new Category() { Name = "Фрезерные станки на серводвигателях" }).AddToBase();
            //(new Category() { Name = "Чиллеры" }).AddToBase();
            MultipleRecordsForCompare multRecords = new MultipleRecordsForCompare(records);
            return View(multRecords);
        }

        public ActionResult RecordsOfCategory(int categoryID)
        {
            int userID = Access.GetUserID(User, HttpContext);
            if (Access.CanReadCategory(userID, categoryID))
            {
                return View(TESTRecords.GetRecords());
            }
            return RedirectToAction("Index", "Home");
        }
    }
}