using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class CountriesAndLanguages
    {
        public List<CountryAndLanguageModel.Country> Countries { get; set; }
        public List<CountryAndLanguageModel.Countrylanguage> CountryLanguages { get; set; }
    }
}