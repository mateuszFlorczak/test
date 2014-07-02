using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult countries()
        {
            return View();
        }
        public ActionResult groupedCountries()
        {
            return View();
        }
        public ActionResult country(string CountryCode)
        {
            return View();
        }
    }
}