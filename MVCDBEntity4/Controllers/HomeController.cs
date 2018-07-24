using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDBEntity4.Models;
using System.Text;

namespace MVCDBEntity4.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            EmployeeContext emp = new EmployeeContext();
            ViewBag.Department = new SelectList(emp.tbl_department_list, "Id", "Name");
            Employee employee = emp.Employees.Single(x => x.Id == 17);
            ViewBag.Name = employee.Name;
            return View();
        }

        //strongly typed cpde: code iw written in view so that errors can be easily detected
        public ActionResult Index1()
        {
            // EmployeeContext emp = new EmployeeContext();
            Employee2 emp = new Employee2("Sunil");
            return View(emp);
        }

        [HttpGet]   
        [ActionName("RadioList")]     
        public ActionResult RadioList_get()
        {
            // EmployeeContext emp = new EmployeeContext();
            Employee2 emp = new Employee2("Sunil");
            return View(emp);
        }

        [HttpPost]
        [ActionName("RadioList")]
        public string RadioList_post()
        {
            Employee2 employee = new Employee2("AA");
            TryUpdateModel(employee);
            if (string.IsNullOrEmpty(employee.SelectedDept))
            {
                return "No dept Selected";
            }
            else
            {
                return "Dept Selected With ID:" + employee.SelectedDept;
            }
        }

        //checkbox list
        [HttpGet]
        [ActionName("CheckBox")]
        public ActionResult CheckBox_get()
        {
            EmployeeContext empctx = new EmployeeContext();
            return View(empctx.Cities);
        }

        [HttpPost]
        [ActionName("CheckBox")]
        public string CheckBox_post(IEnumerable<City>cities)
        {
            EmployeeContext empctx = new EmployeeContext();
            if(cities.Count(x =>x.IsSelected)==0)
            {
                return "city not selected.";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("You Selected.");
                foreach(City city in cities)
                {
                    if(city.IsSelected)
                    {
                        sb.Append(city.Name + ", ");
                    }
                }
                sb.Remove(sb.ToString().LastIndexOf(","), 1);
                return sb.ToString();
            }
            
        }


        //drop list
        [HttpGet]
        //[ActionName("ListBox")]
        public ActionResult ListBox()
        {
            EmployeeContext empctx = new EmployeeContext();           
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            foreach(City emp in empctx.Cities)
            {
                SelectListItem item = new SelectListItem()
                {
                    Text = emp.Name,
                    Value = emp.Id.ToString(),
                    Selected = emp.IsSelected
                };
                selectListItem.Add(item);
            }

            CitiesModel citiesModel = new CitiesModel();
            citiesModel.Cities = selectListItem;
            return View(citiesModel);
        }

        [HttpPost]
       // [ActionName("ListBox")]
        public string ListBox(IEnumerable<string> str)
        {

            if (str ==null)
            {
                return "No selection";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("You Selected:" + string.Join(",", str));

                return sb.ToString();
            }

        }
    }
}