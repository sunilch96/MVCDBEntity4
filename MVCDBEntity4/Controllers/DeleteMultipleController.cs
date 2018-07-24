using MVCDBEntity4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDBEntity4.Controllers
{
    public class DeleteMultipleController : Controller
    {
        private EmployeeContext db = new EmployeeContext();
        // GET: DeleteMultiple
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> id_list)
        {
            //  db.Employees.Where(x => id_list.Contains(x.Id)).ToList().ForEach(db.Employees.Remove());
            var emp = db.Employees.Where(x => id_list.Contains(x.Id)).ToList();
            db.Employees.RemoveRange(emp);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}