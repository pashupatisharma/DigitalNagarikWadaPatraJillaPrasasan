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
using System.IO;

namespace HelpDesk.Controllers
{
    [Authorize]
    public class PeopleRepresentativesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PeopleRepresentatives
        public Task<ActionResult> Index(int? page, int? pageSize, string sortOrder, string PRName, int? PostId)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.PRName = PRName;
            ViewBag.PostId = PostId;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = (sortOrder == "Date") ? "date_desc" : "Date";

            var Lists = from m in db.PeopleRepresentative.OrderByDescending(m => m.Id)
                        select m;

            if (!string.IsNullOrEmpty(PRName))
            {
                Lists = Lists.Where(x => x.Name.Contains(PRName));
            }
            if (PostId > 0)
            {
                Lists = Lists.Where(x => x.PostId == PostId);
            }

            var num = Lists.Count<PeopleRepresentative>();
            int num2 = 10;
            if (pageSize.HasValue)
            {
                num2 = pageSize.Value;
            }

            int? nullable = null;
            int pageNumber = nullable.HasValue ? nullable.GetValueOrDefault() : 1;
            return Task.FromResult<ActionResult>(View(Lists.ToPagedList<PeopleRepresentative>(pageNumber, num2)));
        }

        // GET: PeopleRepresentatives/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PeopleRepresentative peopleRepresentative = await db.PeopleRepresentative.FindAsync(id);
            if (peopleRepresentative == null)
            {
                return HttpNotFound();
            }
            return View(peopleRepresentative);
        }

        // GET: PeopleRepresentatives/Create
        public ActionResult Create()
        {
            PeopleRepresentative model = new PeopleRepresentative();
            return View(model);
        }

        // POST: PeopleRepresentatives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PeopleRepresentative model)
        {
            bool sts = ModelState.IsValid;
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
                        string folderPath = Server.MapPath("~/JanaPratinidhiImages/");
                        if (!Directory.Exists(folderPath))
                        {
                            //If Directory (Folder) does not exists. Create it.
                            Directory.CreateDirectory(folderPath);
                        }

                        var path = Path.Combine(Server.MapPath("~/JanaPratinidhiImages"), fileName);

                        if (System.IO.File.Exists(path))
                        {
                            //ViewBag.Message = "This file is already exist!";
                            ModelState.AddModelError("Image", "This image is already exist!");
                            //TempData["AlertMessage"] = "my alert message";
                            //return RedirectToAction("UploadFile", "UploadDownload");
                            return View(model);
                        }
                        else
                        {
                            model.Image = fileName;
                            model.ImageUrl = path;
                            file.SaveAs(path);
                        }
                    }
                }

                if (model.DateOfBirth != null)
                    model.DateOfBirthEng = NepaliDateConverter.ConvertToEnglish(NepaliDateConverter.Format(model.DateOfBirth));

                if (model.AppointmentDate != null)
                    model.AppointmentDateEng = NepaliDateConverter.ConvertToEnglish(NepaliDateConverter.Format(model.AppointmentDate));

                model.EmpTypeId = 1;
                db.PeopleRepresentative.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return View(model);
        }

        // GET: PeopleRepresentatives/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PeopleRepresentative peopleRepresentative = await db.PeopleRepresentative.FindAsync(id);
            peopleRepresentative.EmpTypeId = 1;
            if (peopleRepresentative == null)
            {
                return HttpNotFound();
            }
            return View(peopleRepresentative);
        }

        // POST: PeopleRepresentatives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PeopleRepresentative peopleRepresentative)
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
                        var path = Path.Combine(Server.MapPath("~/JanaPratinidhiImages"), fileName);

                        if (System.IO.File.Exists(path))
                        {
                            //ViewBag.Message = "This file is already exist!";
                            ModelState.AddModelError("Image", "This image is already exist!");
                            //TempData["AlertMessage"] = "my alert message";
                            //return RedirectToAction("UploadFile", "UploadDownload");
                            return View(peopleRepresentative);
                        }
                        else
                        {
                            peopleRepresentative.Image = fileName;
                            peopleRepresentative.ImageUrl = path;
                            file.SaveAs(path);
                        }
                    }
                }

                if (peopleRepresentative.DateOfBirth != null)
                    peopleRepresentative.DateOfBirthEng = NepaliDateConverter.ConvertToEnglish(NepaliDateConverter.Format(peopleRepresentative.DateOfBirth));

                if (peopleRepresentative.AppointmentDate != null)
                    peopleRepresentative.AppointmentDateEng = NepaliDateConverter.ConvertToEnglish(NepaliDateConverter.Format(peopleRepresentative.AppointmentDate));


                peopleRepresentative.EmpTypeId = 1;
                db.Entry(peopleRepresentative).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(peopleRepresentative);
        }

        // GET: PeopleRepresentatives/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PeopleRepresentative peopleRepresentative = await db.PeopleRepresentative.FindAsync(id);
            if (peopleRepresentative == null)
            {
                return HttpNotFound();
            }
            return View(peopleRepresentative);
        }

        // POST: PeopleRepresentatives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string fileName = DeleteImage(id);
            var path = Path.Combine(Server.MapPath("~/JanaPratinidhiImages"), fileName);

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
                var objToremove = db.PeopleRepresentative.Where(x => x.Id == id).FirstOrDefault();
                db.PeopleRepresentative.Remove(objToremove);
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
