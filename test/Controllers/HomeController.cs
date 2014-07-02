using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CountryAndLanguageModel;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        private CountryAndLanguageModel.CountryAndLanguageEntities db = new CountryAndLanguageModel.CountryAndLanguageEntities();
        // GET: Home
        public HomeController() { }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult countries()
        {
            List<Country> Countries = db.Countries.ToList();
            List<Countrylanguage> CountryLanguages = db.Countrylanguages.ToList();
            
            Models.CountriesAndLanguages CAL = new Models.CountriesAndLanguages();

            CAL.Countries = (from Country in Countries
                             join Countrylanguage in CountryLanguages on Country.Code equals Countrylanguage.Countrycode
                             where Countrylanguage.Isofficial == true
                             orderby Country.Continent ascending,
                                     Country.Name ascending,
                                     Countrylanguage.Percentage descending
                             select Country).ToList();
            CAL.CountryLanguages = (from Country in Countries
                                    join Countrylanguage in CountryLanguages on Country.Code equals Countrylanguage.Countrycode
                                    where Countrylanguage.Isofficial == true
                                    orderby Country.Continent ascending,
                                            Country.Name ascending,
                                            Countrylanguage.Percentage descending
                                    select Countrylanguage).ToList();

            return View(CAL);
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