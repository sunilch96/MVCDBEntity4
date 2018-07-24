using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDBEntity4.Models
{
    public class Employee2
    {
        private string _name;
        public Employee2(string name)
        {
            this._name = name;
        }

        public string SelectedDept { get; set; }
        public List<tbl_department_list>Departmentlist
        {
            get
            {
                EmployeeContext empdb = new EmployeeContext();
                return empdb.tbl_department_list.ToList();
            }
        }

        public string CompanyName
        {
            get { return _name; }
            set { _name = value; }
        }

        //checkbox list
        public IEnumerable<SelectListItem> Cities { get; set; }
        public IEnumerable<string> SelectCities { get; set; }

    }
}