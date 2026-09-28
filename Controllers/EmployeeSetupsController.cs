using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelpDesk.Models;
using PagedList;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.IO;

namespace HelpDesk.Controllers
{
    [Authorize]
    public class EmployeeSetupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EmployeeSetups
        public ActionResult Index(string sortOrder, int? page, int? pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = (sortOrder == "Date") ? "date_desc" : "Date";

            IEnumerable<EmployeeSetup> Lists = db.EmployeeSetup.OrderByDescending(m => m.EmpId).ToList();

            var num = Lists.Count<EmployeeSetup>();
            int num2 = 10;
            if (pageSize.HasValue)
            {
                num2 = pageSize.Value;
            }

            int? nullable = null;
            int pageNumber = nullable.HasValue ? nullable.GetValueOrDefault() : 1;
            return View(Lists.ToPagedList<EmployeeSetup>(pageNumber, num2));
        }

        // GET: EmployeeSetups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSetup employeeSetup = await db.EmployeeSetup.FindAsync(id);
            if (employeeSetup == null)
            {
                return HttpNotFound();
            }
            return View(employeeSetup);
        }

        // GET: EmployeeSetups/Create
        public ActionResult Create()
        {
            EmployeeSetup model = new EmployeeSetup();
            
            return View(model);
        }

        // POST: EmployeeSetups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeSetup employeeSetup)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];

                //var fileName = Path.GetFileName(file.FileName);

                if (file == null)
                {
                    ModelState.AddModelError("Image", "Please Upload Your file");
                }
                else if (file.ContentLength > 0)
                {
                    int MaxContentLength = 2024 * 2024; // 4MB

                    string[] AllowedFileExtensions = new string[] { ".png", ".PNG", ".jpeg", ".jpg", ".JPG", ".JPEG", ".webp" };

                    if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("Image", "Please file of type: " + string.Join(", ", AllowedFileExtensions));
                    }

                    else if (file.ContentLength > MaxContentLength)
                    {
                        ModelState.AddModelError("Image", "Your Image is too large, maximum allowed size is: " + MaxContentLength + " MB. Please upload smaller size image");
                    }
                    else
                    {
                        //TO:DO

                        var fileName = Path.GetFileName(file.FileName);
                        //string fname = User.Identity.Name + "@" + fileName;


                        //Check whether Directory (Folder) exists.
                        string folderPath = Server.MapPath("~/EmployeeImage/");
                        if (!Directory.Exists(folderPath))
                        {
                            //If Directory (Folder) does not exists. Create it.
                            Directory.CreateDirectory(folderPath);
                        }
                        var path = Path.Combine(Server.MapPath("~/EmployeeImage"), fileName);

                        if (System.IO.File.Exists(path))
                        {
                            //ViewBag.Message = "This file is already exist!";
                            ModelState.AddModelError("Image", "This image is already exist!");
                            //TempData["AlertMessage"] = "my alert message";
                            //return RedirectToAction("UploadFile", "UploadDownload");
                            return View(employeeSetup);
                        }
                        else
                        {
                            employeeSetup.Image = fileName;
                            employeeSetup.ImageUrl = path;
                            file.SaveAs(path);
                        }
                    }
                }

                if (employeeSetup.AppointmentDate != null)
                {
                    employeeSetup.AppointmentDateEng = NepaliDateConverter.ConvertToEnglish(NepaliDateConverter.Format(employeeSetup.AppointmentDate));
                }
                if (employeeSetup.DateOfBirth != null)
                {
                    employeeSetup.DateOfBirthEng = NepaliDateConverter.ConvertToEnglish(NepaliDateConverter.Format(employeeSetup.DateOfBirth));
                }
                employeeSetup.EmpTypeId = 2;
                db.EmployeeSetup.Add(employeeSetup);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(employeeSetup);
        }

        // GET: EmployeeSetups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSetup employeeSetup = await db.EmployeeSetup.FindAsync(id);
            employeeSetup.EmpTypeId = 2;
            if (employeeSetup == null)
            {
                return HttpNotFound();
            }
            return View(employeeSetup);
        }

        // POST: EmployeeSetups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EmployeeSetup employeeSetup)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];

                //var fileName = Path.GetFileName(file.FileName);

                if (file == null)
                {
                    ModelState.AddModelError("Image", "Please Upload Your file");
                }
                else if (file.ContentLength > 0)
                {
                    int MaxContentLength = 2024 * 2024; // 4MB

                    string[] AllowedFileExtensions = new string[] { ".png", ".PNG", ".jpeg", ".jpg", ".JPG", ".JPEG", ".webp" };

                    if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("Image", "Please file of type: " + string.Join(", ", AllowedFileExtensions));
                    }

                    else if (file.ContentLength > MaxContentLength)
                    {
                        ModelState.AddModelError("Image", "Your Image is too large, maximum allowed size is: " + MaxContentLength + " MB. Please upload smaller size image");
                    }
                    else
                    {
                        //TO:DO

                        var fileName = Path.GetFileName(file.FileName);
                        //string fname = User.Identity.Name + "@" + fileName;
                        var path = Path.Combine(Server.MapPath("~/EmployeeImage"), fileName);

                        if (System.IO.File.Exists(path))
                        {
                            //ViewBag.Message = "This file is already exist!";
                            ModelState.AddModelError("Image", "This image is already exist!");
                            //TempData["AlertMessage"] = "my alert message";
                            //return RedirectToAction("UploadFile", "UploadDownload");
                            return View(employeeSetup);
                        }
                        else
                        {
                            employeeSetup.Image = fileName;
                            employeeSetup.ImageUrl = path;
                            file.SaveAs(path);
                        }
                    }
                }

                if (employeeSetup.AppointmentDate != null)
                {
                    employeeSetup.AppointmentDateEng = NepaliDateConverter.ConvertToEnglish(NepaliDateConverter.Format(employeeSetup.AppointmentDate));
                }
                if (employeeSetup.DateOfBirth != null)
                {
                    employeeSetup.DateOfBirthEng = NepaliDateConverter.ConvertToEnglish(NepaliDateConverter.Format(employeeSetup.DateOfBirth));
                }

                employeeSetup.EmpTypeId = 2;
                db.Entry(employeeSetup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(employeeSetup);
        }

        // GET: EmployeeSetups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSetup employeeSetup = await db.EmployeeSetup.FindAsync(id);
            if (employeeSetup == null)
            {
                return HttpNotFound();
            }
            return View(employeeSetup);
        }

        // POST: EmployeeSetups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string fileName = DeleteImage(id);
            var path = Path.Combine(Server.MapPath("~/EmployeeImage"), fileName);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);

            }
            return RedirectToAction("Index");
        }

        public string DeleteImage(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                var objToremove = db.EmployeeSetup.Where(x => x.EmpId == id).FirstOrDefault();
                db.EmployeeSetup.Remove(objToremove);
                db.SaveChanges();
                return objToremove.Image;
            }
            catch (Exception ex)
            { }

            return "";
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
