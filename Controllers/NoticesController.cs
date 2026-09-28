using HelpDesk.Models;
using SchoolWeb.Areas.Administrator.Provider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mime;
using System.Data.Entity;
using System.Net;

namespace HelpDesk.Controllers
{
    [Authorize]
    public class NoticesController : Controller
    {
        private NoticeProvider provider = new NoticeProvider();
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            Notice notice = new Notice();
            notice.Lists = new List<Notice>();
            notice.Lists = ((IQueryable<Notice>)this.db.Notice).OrderByDescending(x => x.NoticeId).ToList();
            return (ActionResult)this.View((object)notice);
        }

        public ActionResult Create()
        {
            return (ActionResult)this.View((object)new Notice()
            {
                NoticeFiles = new NoticeFiles()
            });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Notice model)
        {
            model.NoticeFiles = new NoticeFiles();
            if (!this.ModelState.IsValid)
                return (ActionResult)this.View((object)model);
            HttpContext context = System.Web.HttpContext.Current;

            HttpFileCollection files = context.Request.Files;
            for (int index = 0; index < files.Count; ++index)
            {
                HttpPostedFile httpPostedFile = files[index];
                if (httpPostedFile == null)
                    this.ModelState.AddModelError("Image", "Please Upload Your file");
                else if (httpPostedFile.ContentLength > 0)
                {
                    int num = 4096576;
                    string[] source = new string[7]
                    {
            ".png",
            ".PNG",
            ".jpeg",
            ".jpg",
            ".JPG",
            ".JPEG",
            ".webp"
                    };
                    if (!((IEnumerable<string>)source).Contains<string>(httpPostedFile.FileName.Substring(httpPostedFile.FileName.LastIndexOf('.'))))
                        this.ModelState.AddModelError("Image", "Please file of type: " + string.Join(", ", source));
                    else if (httpPostedFile.ContentLength > num)
                    {
                        this.ModelState.AddModelError("Image", "Your Image is too large, maximum allowed size is: " + num.ToString() + " MB. Please upload smaller size image");
                    }
                    else
                    {
                        string fileName = Path.GetFileName(httpPostedFile.FileName);
                        string path = this.Server.MapPath("~/Notice_Upload/");
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        string str = Path.Combine(this.Server.MapPath("~/Notice_Upload"), fileName);
                        if (System.IO.File.Exists(str))
                        {
                            this.ModelState.AddModelError("Image", "This image is already exist!");
                            return (ActionResult)this.View((object)model);
                        }
                        this.db.NoticeFiles.Add(new NoticeFiles()
                        {
                            Image = fileName,
                            NoticeId = new int?(model.NoticeId),
                            ImageUrl = str
                        });
                        httpPostedFile.SaveAs(str);
                    }
                }
            }
            this.db.Notice.Add(model);
            ((DbContext)this.db).SaveChangesAsync();
            return (ActionResult)this.RedirectToAction("Index");
        }

        public byte[] ReadAllBytes(string fileName)
        {
            byte[] buffer = (byte[])null;
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, (int)fileStream.Length);
            }
            return buffer;
        }

        public ActionResult Download(int id)
        {
            Notice notice = ((IQueryable<Notice>)new ApplicationDbContext().Notice.Where(x => x.NoticeId == id)).FirstOrDefault<Notice>();
            if (notice == null || notice.NoticeFiles.Image == null)
                return (ActionResult)null;
            byte[] numArray = this.ReadAllBytes(this.Server.MapPath(this.Url.Content("~/Notice_Upload/" + notice.NoticeFiles.Image)));
            this.Response.AppendHeader("Content-Disposition", new ContentDisposition()
            {
                FileName = notice.NoticeFiles.Image,
                Inline = true
            }.ToString());
            return (ActionResult)this.File(numArray, notice.NoticeFiles.Image);
        }

        public ActionResult Detail(int? id)
        {
            if (!id.HasValue)
                return (ActionResult)new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Notice notice = this.db.Notice.Find(new object[1]
            {
        (object) id
            });
            return notice == null ? (ActionResult)this.HttpNotFound() : (ActionResult)this.View((object)notice);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return (ActionResult)new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Notice notice = this.db.Notice.Find(new object[1]
            {
        (object) id
            });
            notice.NoticeFiles = new NoticeFiles();
            notice.NoticeFiles = ((IQueryable<NoticeFiles>)this.db.NoticeFiles.Where(x => x.NoticeId == id)).FirstOrDefault<NoticeFiles>();
            return notice == null ? (ActionResult)this.HttpNotFound() : (ActionResult)this.View((object)notice);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Notice model)
        {
            model.NoticeFiles = new NoticeFiles();
            if (this.ModelState.IsValid)
            {
                HttpContext context = System.Web.HttpContext.Current;
                HttpFileCollection files = context.Request.Files;
                int index = 0;
                if (index < files.Count)
                {
                    HttpPostedFile httpPostedFile = files[index];
                    if (httpPostedFile == null)
                        this.ModelState.AddModelError("Image", "Please Upload Your file");
                    else if (httpPostedFile.ContentLength > 0)
                    {
                        int num = 4096576;
                        string[] source = new string[7]
                        {
              ".png",
              ".PNG",
              ".jpeg",
              ".jpg",
              ".JPG",
              ".JPEG",
              ".webp"
                        };
                        if (!((IEnumerable<string>)source).Contains<string>(httpPostedFile.FileName.Substring(httpPostedFile.FileName.LastIndexOf('.'))))
                            this.ModelState.AddModelError("Image", "Please file of type: " + string.Join(", ", source));
                        else if (httpPostedFile.ContentLength > num)
                        {
                            this.ModelState.AddModelError("Image", "Your Image is too large, maximum allowed size is: " + num.ToString() + " MB. Please upload smaller size image");
                        }
                        else
                        {
                            string fileName = Path.GetFileName(httpPostedFile.FileName);
                            string str = Path.Combine(this.Server.MapPath("~/Notice_Upload"), fileName);
                            if (System.IO.File.Exists(str))
                            {
                                NoticeFiles noticeFiles = new NoticeFiles();
                                this.ModelState.AddModelError("Image", "This image is already exist!");
                                ((DbContext)this.db).Entry<NoticeFiles>(noticeFiles).State = (EntityState)16;
                                httpPostedFile.SaveAs(str);
                                ((DbContext)this.db).SaveChangesAsync();
                                return (ActionResult)this.View((object)model);
                            }
                            this.db.NoticeFiles.Add(new NoticeFiles()
                            {
                                Image = fileName,
                                NoticeId = new int?(model.NoticeId),
                                ImageUrl = str
                            });
                            httpPostedFile.SaveAs(str);
                        }
                    }
                  ((DbContext)this.db).Entry<Notice>(model).State = (EntityState)16;
                    ((DbContext)this.db).SaveChangesAsync();
                    return (ActionResult)this.RedirectToAction("Index");
                }
            }
            return (ActionResult)this.View((object)model);
        }

        public ActionResult Delete(int id)
        {
            this.db.Notice.Remove(((IQueryable<Notice>)this.db.Notice.Where(x => x.NoticeId == id)).FirstOrDefault<Notice>());
            NoticeFiles noticeFiles = new NoticeFiles();
            noticeFiles.lists = new List<NoticeFiles>();
            noticeFiles.lists = ((IQueryable<NoticeFiles>)this.db.NoticeFiles.Where(x => x.NoticeId == (int?)id)).ToList<NoticeFiles>();
            foreach (NoticeFiles list in noticeFiles.lists)
            {
                string path2 = this.provider.DeleteNoticeFiles(list.NoticeFilesId);
                if (path2 != null)
                {
                    string path = Path.Combine(this.Server.MapPath("~/Notice_Upload/"), path2);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }
            }
          ((DbContext)this.db).SaveChanges();
            return (ActionResult)this.RedirectToAction("Index");
        }
    }
}

