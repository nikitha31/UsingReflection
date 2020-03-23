using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsingReflection.Models;

namespace UsingReflection.Controllers
{
    [Authorizing]
    public class EmployeeController : Controller
    {
        // GET: Employee
        private ReflectionsEntities db = new ReflectionsEntities();
        public ActionResult List()
        {
            return View(db.EmployDetails.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployDetail employeeDetail)
        {
            if (ModelState.IsValid)
            {
                db.EmployDetails.Add(employeeDetail);
                db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(employeeDetail);
        }

        public ActionResult Edit(int id)
        {
            EmployDetail product = db.EmployDetails.Find(id);
            return View();
        }

        [HttpPost]
        public ActionResult Edit(EmployDetail employeeDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(employeeDetail);

        }

        public ActionResult Delete(int id)
        {
            EmployDetail employee = db.EmployDetails.Find(id);
            db.EmployDetails.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("List");
        }


    }
}