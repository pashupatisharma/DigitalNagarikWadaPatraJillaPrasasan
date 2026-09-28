using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HelpDesk.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Create()
        {
            Employee model = new Employee();
            var data = db.Employee.FirstOrDefault();
            if (data != null) { model = data; }
            return View(model);
        }

        // POST: Offices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.Photo1 != null)
                {
                    employee.AdkshyaPhoto = Utility.ReturnByte(employee.Photo1);
                }

                if (employee.Photo2 != null)
                {
                    employee.UpaAdkshyaPhoto = Utility.ReturnByte(employee.Photo2);
                }

                if (employee.Photo3 != null)
                {
                    employee.AdhikritPhoto = Utility.ReturnByte(employee.Photo3);
                }
                if (employee.Photo4 != null)
                {
                    employee.ItPhoto = Utility.ReturnByte(employee.Photo4);
                }
                if (employee.Id > 0)
                {
                    db.Entry(employee).State = EntityState.Modified;
                }
                else
                {
                    db.Employee.Add(employee);
                }
                await db.SaveChangesAsync();
                return RedirectToAction("Create");
            }

            return View(employee);
        }

        // GET: Offices/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offices offices = await db.Offices.FindAsync(id);
            if (offices == null)
            {
                return HttpNotFound();
            }
            return View(offices);
        }

        // POST: Offices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.Photo1 != null)
                {
                    employee.AdkshyaPhoto = Utility.ReturnByte(employee.Photo1);
                }

                if (employee.Photo2 != null)
                {
                    employee.UpaAdkshyaPhoto = Utility.ReturnByte(employee.Photo2);
                }

                if (employee.Photo3 != null)
                {
                    employee.AdhikritPhoto = Utility.ReturnByte(employee.Photo3);
                }

                db.Entry(employee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(employee);
        }
    }
}