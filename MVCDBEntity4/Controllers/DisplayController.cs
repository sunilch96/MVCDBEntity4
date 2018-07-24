using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDBEntity4.Models;
using MVCDBEntity4.Common;

namespace MVCDBEntity4.Controllers
{
    [HandleError]
    public class DisplayController : Controller
    {
        // GET: Display
        public ActionResult Index(int id)
        {
            EmployeeContext empctx = new EmployeeContext();
            EmployeeDetail ed = empctx.EmployeeDetails.Single(x => x.Id == id);
            return View(ed);
        }

        public ActionResult Edit(int id)
        {
            EmployeeContext empctx = new EmployeeContext();
            EmployeeDetail ed = empctx.EmployeeDetails.Single(x => x.Id == id);
            return View(ed);
        }

        //[OutputCache(Duration = 10)]
        //[OutputCache(CacheProfile = "samplecache")] //webconfig cache - if used with child methods will give error
        public ActionResult List()
        {
            //System.Threading.Thread.Sleep(3000);
            EmployeeContext empctx = new EmployeeContext();           
            return View(empctx.EmployeeDetails.ToList());
        }

        // [OutputCache(Duration = 10)]
        [ChildActionOnly] 
        [PartialCache("cacheforchild")]      
        public string OutputCache()
        {            
            EmployeeContext empctx = new EmployeeContext();
            string _str = "Total Employees: " + empctx.EmployeeDetails.ToList().Count().ToString() + " @ " + DateTime.Now.ToString();
            return _str;
        }
    }
}