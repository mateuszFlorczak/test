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
            List<countrylanguage> countryLanguages = db.countrylanguage.ToList();
            List<country> Countries = db.country.ToList();

            List<Models.WorldLanguages> WL = new List<Models.WorldLanguages>();
            long population = 0;
            foreach (var countr in Countries)
            {
                population += countr.population;
            }

            WL = (from Countrylanguage in countryLanguages
                  join Country in Countries on Countrylanguage.countrycode equals Country.code
                  group new { Countrylanguage, Country } by Countrylanguage.language into Languages
                  select new Models.WorldLanguages
                  {
                      LanguageName = Languages.Key,
                      Population = Languages.Sum(x => x.Country.population),
                      Percentage = 1//(float)Math.Round((double)(Languages.Sum(x => x.Country.Population) / population), 2)
                  }).OrderByDescending((x => x.Population)).ThenBy(x => x.LanguageName).ToList();

            return View(WL);
        }
        public ActionResult country(string CountryCode)
        {
            if (CountryCode == null)
            {

            }
            else if (CountryCode.Count() != 3)
            {

            }
            country country = (from Country in db.country
                               where Country.code == CountryCode
                               select Country).First();
            ViewBag.CountryName = country.name;
            List<countrylanguage> countryLanguages = (from CountryLanguage in db.countrylanguage
                                                      where CountryLanguage.countrycode == CountryCode
                                                      select CountryLanguage).ToList();
            return View(countryLanguages);
        }
    }
}