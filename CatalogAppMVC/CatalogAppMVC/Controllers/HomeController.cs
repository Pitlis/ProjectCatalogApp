using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CatalogAppMVC.Models.interfaces;
using CatalogAppMVC.Models.TESTCODE;
using CatalogAppMVC.Models;

namespace CatalogAppMVC.Controllers
{
    public class HomeController : Controller
    {
        IUser user;//активный пользователь


        // GET: Home
        public ActionResult Index()
        {
            return View(TestICategory.GetOpenCategory(user));
        }
    }
}