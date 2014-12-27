using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CatalogAppMVC.Models.interfaces;
using CatalogAppMVC.Models.TESTCODE;
using CatalogAppMVC.Models;
using CatalogAppMVC.Models.Identity;

namespace CatalogAppMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            IMyAppAuthentication user = new ApplicationAuthentication(HttpContext);
            return View(TestICategory.GetOpenCategory(user));
        }
    }
}