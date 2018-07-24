using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDBEntity4.Models
{
    public class CitiesModel
    {
        //checkbox list
        public IEnumerable<SelectListItem> Cities { get; set; }
        public IEnumerable<string> SelectCities { get; set; }
    }
}