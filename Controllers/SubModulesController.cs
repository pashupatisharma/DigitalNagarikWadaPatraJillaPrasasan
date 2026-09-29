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
using System.IO;

namespace HelpDesk.Controllers
{
    [Authorize]
    public class SubModulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SubModules
        public async Task<ActionResult> Index()
        {
            var subModule = db.SubModule.Include(s => s.MainModule);
            return View(await subModule.ToListAsync());
        }

        // GET: SubModules/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubModule subModule = await db.SubModule.FindAsync(id);
            if (subModule == null)
            {
                return HttpNotFound();
            }
            return View(subModule);
        }

        // GET: SubModules/Create
        [ValidateInput(false)]
        public ActionResult Create()
        {
            SubModule subModule = new SubModule();
            ViewBag.MainModuleId = new SelectList(db.MainModule, "Id", "Title");
            return View(subModule);
        }

        // POST: SubModules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( SubModule subModule , HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (fileupload != null)
                    {
                        subModule.FileName = UploadVideo(fileupload);

                        //Check whether Directory (Folder) exists.
                        string folderPath = Server.MapPath("~/VideoFileUpload/");
                        if (!Directory.Exists(folderPath))
                        {
                            //If Directory (Folder) does not exists. Create it.
                            Directory.CreateDirectory(folderPath);
                        }
                        subModule.FilePath = "~/VideoFileUpload/" + subModule.FileName;
                    }
                    db.SubModule.Add(subModule);
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message.ToString());
                    return View(subModule);
                }
                return RedirectToAction("Index");
            }

            ViewBag.MainModuleId = new SelectList(db.MainModule, "Id", "Title", subModule.MainModuleId);
            return View(subModule);
        }

      
        public string UploadVideo(HttpPostedFileBase fileupload)
        {
            if (fileupload != null)
            {
                string fileName = Path.GetFileName(fileupload.FileName);
                int fileSize = fileupload.ContentLength;
                int Size = fileSize / 1000;
                var uniqueFileName = Guid.NewGuid().ToString() + fileName;
                fileupload.SaveAs(Server.MapPath("~/VideoFileUpload/" + uniqueFileName));

                return uniqueFileName;
            }
            return "";

                
        }



        // GET: SubModules/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubModule subModule = await db.SubModule.FindAsync(id);
            if (subModule == null)
            {
                return HttpNotFound();
            }
            ViewBag.MainModuleId = new SelectList(db.MainModule, "Id", "Title", subModule.MainModuleId);
            return View(subModule);
        }

        // POST: SubModules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      //  [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit( SubModule subModule, HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (fileupload != null)
                    {
                        subModule.FileName = UploadVideo(fileupload);
                        subModule.FilePath = "~/VideoFileUpload/" + subModule.FileName;
                    }
                    db.Entry(subModule).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message.ToString());
                    return View(subModule);
                }
                return RedirectToAction("Index");
            }
            ViewBag.MainModuleId = new SelectList(db.MainModule, "Id", "Title", subModule.MainModuleId);
            return View(subModule);
        }

        // GET: SubModules/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubModule subModule = await db.SubModule.FindAsync(id);
            if (subModule == null)
            {
                return HttpNotFound();
            }
            return View(subModule);
        }

        // POST: SubModules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SubModule subModule = await db.SubModule.FindAsync(id);
            db.SubModule.Remove(subModule);
            await db.SaveChangesAsync();
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
