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
using System.Xml.Linq;
using System.IO;

namespace HelpDesk.Controllers
{
    [Authorize]
    public class VideosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Videos
        public ActionResult Index(string sortOrder, int? page, int? pageSize, string VideoName)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.VideoName = VideoName;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = (sortOrder == "Date") ? "date_desc" : "Date";

            var Lists = from m in db.Video.OrderByDescending(m => m.VideoId)
                        select m;

            if (!string.IsNullOrEmpty(VideoName))
            {
                Lists = Lists.Where(x => x.VideoName.Contains(VideoName));
            }

            var num = Lists.Count<Video>();
            int num2 = 9;
            if (pageSize.HasValue)
            {
                num2 = pageSize.Value;
            }

            int? nullable = null;
            int pageNumber = nullable.HasValue ? nullable.GetValueOrDefault() : 1;
            return View(Lists.ToPagedList<Video>(pageNumber, num2));
        }

        // GET: Videos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = await db.Video.FindAsync(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // GET: Videos/Create
        public ActionResult Create()
        {
            Video model = new Video();

            return View(model);
        }

        // POST: Videos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Video video)
        {
            bool sts = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["UploadedVideo"];

                //var fileName = Path.GetFileName(file.FileName);

                if (file == null)
                {
                    ModelState.AddModelError("VideoData", "Please Upload Your file");
                }
                else if (file.ContentLength > 0)
                {
                    long num = 5024;
                    long MaxContentLength = num * 1024 * 1024; // 650MB

                    string[] AllowedFileExtensions = new string[] { ".mp4", ".MP4", ".m4p", ".m4v", ".mpg", ".mpeg", ".gif", ".webm", ".avi" };

                    if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("VideoData", "Please video of type: " + string.Join(", ", AllowedFileExtensions));
                    }

                    //else if (file.ContentLength > MaxContentLength)
                    //{
                    //    ModelState.AddModelError("VideoData", "Your Video is too large, maximum allowed size is: " + MaxContentLength + " MB. Please upload smaller size Video!!");
                    //}
                    else
                    {
                        //TO:DO

                        var fileName = Path.GetFileName(file.FileName);
                        //string fname = User.Identity.Name + "@" + fileName;


                        //Check whether Directory (Folder) exists.
                        string folderPath = Server.MapPath("~/UploadedVideos/");
                        if (!Directory.Exists(folderPath))
                        {
                            //If Directory (Folder) does not exists. Create it.
                            Directory.CreateDirectory(folderPath);
                        }
                        var path = Path.Combine(Server.MapPath("~/UploadedVideos"), fileName);


                        if (System.IO.File.Exists(path))
                        {
                            //ViewBag.Message = "This file is already exist!";
                            ModelState.AddModelError("VideoData", "This image is already exist!");
                            //TempData["AlertMessage"] = "my alert message";
                            //return RedirectToAction("UploadFile", "UploadDownload");
                            return View(video);
                        }
                        else
                        {
                            video.VideoData = fileName;
                            video.VideoUrl = path;
                            file.SaveAs(path);
                        }
                    }
                }
                video.UploadedDate = DateTime.Now;
                db.Video.Add(video);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(video);
        }

        // GET: Videos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = await db.Video.FindAsync(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Edit(Video video)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["UploadedVideo"];

                //var fileName = Path.GetFileName(file.FileName);

                if (file == null)
                {
                    ModelState.AddModelError("VideoData", "Please Upload Your file");
                }
                else if (file.ContentLength > 0)
                {
                    long num = 5024;
                    long MaxContentLength = num * 1024 * 1024; // 650MB

                    string[] AllowedFileExtensions = new string[] { ".mp4", ".MP4", ".m4p", ".m4v", ".mpg", ".mpeg", ".gif", ".webm", ".avi" };

                    if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("VideoData", "Please video of type: " + string.Join(", ", AllowedFileExtensions));
                    }

                    //else if (file.ContentLength > MaxContentLength)
                    //{
                    //    ModelState.AddModelError("VideoData", "Your Video is too large, maximum allowed size is: " + MaxContentLength + " MB. Please upload smaller size Video!!");
                    //}
                    else
                    {
                        //TO:DO

                        var fileName = Path.GetFileName(file.FileName);
                        //string fname = User.Identity.Name + "@" + fileName;
                        var path = Path.Combine(Server.MapPath("~/UploadedVideos"), fileName);

                        if (System.IO.File.Exists(path))
                        {
                            //ViewBag.Message = "This file is already exist!";
                            ModelState.AddModelError("VideoData", "This image is already exist!");
                            //TempData["AlertMessage"] = "my alert message";
                            //return RedirectToAction("UploadFile", "UploadDownload");
                            return View(video);
                        }
                        else
                        {
                            video.VideoData = fileName;
                            video.VideoUrl = path;
                            file.SaveAs(path);
                        }
                    }
                }

                video.UploadedDate = DateTime.Now;
                db.Entry(video).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(video);
        }

        // GET: Videos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = await db.Video.FindAsync(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string fileName = DeleteVideos(id);
            var path = Path.Combine(Server.MapPath("~/UploadedVideos"), fileName);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);

            }
            return RedirectToAction("Index");
        }

        public string DeleteVideos(int id)
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
