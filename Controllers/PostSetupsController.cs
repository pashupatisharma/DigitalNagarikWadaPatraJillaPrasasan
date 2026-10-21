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
using System.Xml.Linq;
using System.Collections;
using PagedList;

namespace HelpDesk.Controllers
{
    [Authorize]
    public class PostSetupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PostSetups
        public ActionResult Index(string sortOrder, int? page, int? pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = (sortOrder == "Date") ? "date_desc" : "Date";

            IQueryable<PostSetup> lists = db.PostSetup;

            switch (sortOrder)
            {
                case "name_desc":
                    lists = lists.OrderByDescending(m => m.PostTitle);
                    break;
                case "Date":
                    lists = lists.OrderBy(m => m.PostId);
                    break;
                case "date_desc":
                default:
                    lists = lists.OrderByDescending(m => m.PostId);
                    break;
            }

            int size = pageSize ?? 10;
            int pageNumber = page ?? 1;

            return View(lists.ToPagedList(pageNumber, size));
        }

        // GET: PostSetups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostSetup postSetup = await db.PostSetup.FindAsync(id);
            if (postSetup == null)
            {
                return HttpNotFound();
            }
            return View(postSetup);
        }

        // GET: PostSetups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostSetups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PostId,PostTitle,PostTitleEng")] PostSetup postSetup)
        {
            if (ModelState.IsValid)
            {
                db.PostSetup.Add(postSetup);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(postSetup);
        }

        // GET: PostSetups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostSetup postSetup = await db.PostSetup.FindAsync(id);
            if (postSetup == null)
            {
                return HttpNotFound();
            }
            return View(postSetup);
        }

        // POST: PostSetups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        ///[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PostId,PostTitle,PostTitleEng")] PostSetup postSetup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postSetup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(postSetup);
        }

        // GET: PostSetups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostSetup postSetup = await db.PostSetup.FindAsync(id);
            if (postSetup == null)
            {
                return HttpNotFound();
            }
            return View(postSetup);
        }

        // POST: PostSetups/Delete/5
        [HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PostSetup postSetup = await db.PostSetup.FindAsync(id);
            db.PostSetup.Remove(postSetup);
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
