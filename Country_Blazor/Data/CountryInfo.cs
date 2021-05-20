using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Country_Blazor.Data
{
    public class CountryInfo
    {
        public string Flag { get; set; }
        public string Name { get; set; }
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public string NativeName { get; set; }
        public List<string> AltSpellings { get; set; }
        public List<string> CallingCodes { get; set; }
        public string Capital { get; set; }
        public string Region { get; set; }
        public int Population { get; set; }
        public List<string> Timezones { get; set; }

    }
}
