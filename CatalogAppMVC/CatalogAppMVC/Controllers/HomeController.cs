using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CatalogAppMVC.Models.interfaces;
using CatalogAppMVC.Models.TESTCODE;

namespace CatalogAppMVC.Controllers
{
    public class HomeController : Controller
    {
        IUser user;//активный пользователь
        ICategory categories = new TestICategory();
        ISpecification specification = new TestISpecification();
        IRepository repository = new TESTRepository();


        // GET: Home
        public ActionResult Index()
        {
            return View(categories.GetOpenCategory(user));
        }
    }
}