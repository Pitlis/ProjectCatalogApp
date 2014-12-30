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

        public ActionResult RecordView(int recordID)
        {
            Record record = Record.GetRecord(recordID);
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

        public ActionResult DownloadFile(int FileID)
        {
            CatalogAppMVC.Models.File file = CatalogAppMVC.Models.File.GetFile(FileID);
            if(file != null)
            {
                return File(file.GetPatchToFile(), CatalogAppMVC.Models.File.TYPEFILES, file.GetFileName());
            }
            return RedirectToAction("FileNotFound");
        }

        public ActionResult FileNotFound()
        {
            return View();
        }


        #endregion

        public ActionResult SomeRecordsView()
        {
            List<RecordsViewModel.RecordIdBool> records = Session["RecordsForComparison"] as List<RecordsViewModel.RecordIdBool>;
            List<Record> recordList = new List<Record>();
            foreach(RecordsViewModel.RecordIdBool rec in records)
            {
                if (rec.check)
                    recordList.Add(Record.GetRecord(rec.id));
            }
            MultipleRecordsForCompare multRecords = new MultipleRecordsForCompare(recordList);
            return View(multRecords);
        }
        [HttpGet]
        public ActionResult RecordsOfCategory(int categoryID)
        {
            int userID = Access.GetUserID(User, HttpContext);
            if (Access.CanReadCategory(userID, categoryID))
            {
                ViewBag.CategoryName = Category.GetCategory(categoryID).Name;

                var ViewModel = new RecordsViewModel();
                ViewModel.records = Record.GetRecordsOfCategory(categoryID);
                ViewModel.RecordsForView = new List<RecordsViewModel.RecordIdBool>();
                ViewModel.categoryID = categoryID;
                for (int i = 0; i < ViewModel.records.Count(); ++i)
                {
                    ViewModel.RecordsForView.Add(new RecordsViewModel.RecordIdBool());
                }
                return View(ViewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult RecordsOfCategory(RecordsViewModel model, int CategoryID)
        {
            bool one = false;
            foreach(RecordsViewModel.RecordIdBool rec in model.RecordsForView)
            {
                if(rec.check)
                {
                    Session["RecordsForComparison"] = model.RecordsForView;
                    return RedirectToAction("SomeRecordsView");
                }
            }
            return RedirectToAction("RecordsOfCategory", new { categoryID = CategoryID });
        }
    }
}