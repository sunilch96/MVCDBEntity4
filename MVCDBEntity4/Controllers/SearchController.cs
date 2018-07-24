using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCDBEntity4.Models;
using PagedList.Mvc;
using PagedList;

namespace MVCDBEntity4.Controllers
{
    public class SearchController : Controller
    {
        private EmployeeContext db = new EmployeeContext();

        // GET: Search
        public ActionResult Index(string searchby, string search)
        {
            if(searchby=="Gender")
            {
                return View(db.Employees.Where(x => x.Gender == search || search == null).ToList());
            }
            else
            {
                return View(db.Employees.Where(x => x.Name.StartsWith(search) || search == null).ToList());
            }
        }
       
        //nuget pakage mgr: download PagedList.Mvc
        public ActionResult PagedSearch(string SearchBy, string search, int? Page , string sortBy)//include int? page to get paging
        {
            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";
            ViewBag.SortGenderParameter = sortBy == "Gender" ? "Gender desc" : "Gender";

            var employees = db.Employees.AsQueryable();

            if (SearchBy == "Gender")
            {
                employees = employees.Where(x => x.Gender == search || search == null);
                //return View(db.Employees.Where(x => x.Gender == search || search == null).ToList().ToPagedList(Page ?? 1 , 3));
            }
            else
            {
                employees = employees.Where(x => x.Name.StartsWith(search) || search == null);
                //return View(db.Employees.Where(x => x.Name.StartsWith(search) || search == null).ToList().ToPagedList(Page ?? 1, 3));
            }

            switch(sortBy)
            {
                case "Name desc":
                    employees = employees.OrderByDescending(x => x.Name);
                    break;

                case "Gender desc":
                    employees = employees.OrderByDescending(x => x.Gender);
                    break;

                case "Gender":
                    employees = employees.OrderBy(x => x.Gender);
                    break;

                default:
                    employees = employees.OrderBy(x => x.Name);
                    break;
            }

            return View(employees.ToPagedList(Page ?? 1, 3));
        }

        // GET: Search/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Search/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.tbl_department_list, "Id", "Name");
            return View();
        }

        // POST: Search/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Gender,City,DateOfBirth,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.tbl_department_list, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: Search/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.tbl_department_list, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // POST: Search/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Gender,City,DateOfBirth,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.tbl_department_list, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: Search/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Search/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
