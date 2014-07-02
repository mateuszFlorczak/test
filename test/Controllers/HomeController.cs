using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        private CountryAndLanguageModel db = new CountryAndLanguageModel();
        // GET: Home
        public HomeController() { }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult countries()
        {
            country c = new country();
            
            List<country> Countries =  db.country.ToList();
            List<countrylanguage> CountryLanguages = db.countrylanguage.ToList();
            
            Models.CountriesAndLanguages CAL = new Models.CountriesAndLanguages();
            
            CAL.Countries = (from Country in Countries
                             join Countrylanguage in CountryLanguages on Country.code equals Countrylanguage.countrycode
                             where Countrylanguage.isofficial == true
                             orderby Country.continent ascending,
                                     Country.name ascending,
                                     Countrylanguage.percentage descending
                             select Country).ToList();
            CAL.CountryLanguages = (from Country in Countries
                                    join Countrylanguage in CountryLanguages on Country.code equals Countrylanguage.countrycode
                                    where Countrylanguage.isofficial == true
                                    orderby Country.continent ascending,
                                            Country.name ascending,
                                            Countrylanguage.percentage descending
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