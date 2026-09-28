using SchoolWeb.Areas.Administrator.Provider;
using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Data.Entity;
using System.Web.UI.WebControls;

namespace HelpDesk.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ApplicationDbContext applicationDbContext = new ApplicationDbContext();
        NoticeProvider provider = new NoticeProvider();

        public ActionResult Index()
        {
            int totalPeopleRepresentatives = applicationDbContext.PeopleRepresentative.Count();
            int totalEmployees = applicationDbContext.EmployeeSetup.Count();
            int totalServices = applicationDbContext.SubModule.Count();
            int totalSakha = applicationDbContext.MainModule.Count();

            ViewBag.TotalEmployees = totalEmployees;
            ViewBag.TotalPeopleRepresentatives = totalPeopleRepresentatives;
            ViewBag.TotalServices = totalServices;
            ViewBag.TotalSakha = totalSakha;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult WebIndex()
        {
            ViewBag.Message = "Your contact page.";
            HomePageViewModel model = new HomePageViewModel();

            model.mainModule = new MainModule();
            model.mainModule.List = new List<MainModule>();
            model.mainModule.List = applicationDbContext.MainModule.ToList();

            model.subModule = new SubModule();
            model.subModule.List = new List<SubModule>();
            model.subModule.List = applicationDbContext.SubModule.ToList();

            model.notice = new Notice();
            model.notice.Lists = new List<Notice>();
            model.notice.Lists = applicationDbContext.Notice.ToList();

            model.video = new Video();
            model.video.Lists = new List<Video>();
            model.video.Lists = applicationDbContext.Video.ToList();

            model.employeeSetup = new EmployeeSetup();
            model.employeeSetup.Lists = new List<EmployeeSetup>();
            model.employeeSetup.Lists = applicationDbContext.EmployeeSetup
                                         .OrderBy(e => e.DisplayOrder)
                                         .ToList();

            model.peopleRepresentative = new PeopleRepresentative();
            model.peopleRepresentative.List = applicationDbContext.PeopleRepresentative
                                                .OrderBy(p => p.DisplayOrder)
                                                .ToList();
            return View(model);
        }


        [AllowAnonymous]
        public ActionResult NoticeDetail(int? id)
        {
            if (!id.HasValue)
                return (ActionResult)new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Notice notice = this.applicationDbContext.Notice.Find(new object[1]
            {
        (object) id
            });
            notice.NoticeFiles = new NoticeFiles();
            notice.NoticeFiles.lists = ((IQueryable<NoticeFiles>)this.applicationDbContext.NoticeFiles.Where(x => x.NoticeId == id)).ToList<NoticeFiles>();
            return notice == null ? (ActionResult)this.HttpNotFound() : (ActionResult)this.View((object)notice);
        }
        [AllowAnonymous]
        public ActionResult Service(int? id)
        {
            ViewBag.Message = "Your contact page.";

            SubModule model = new SubModule();
            model.List = new List<SubModule>();
            if (id != null)
                model.List = applicationDbContext.SubModule.Where(x => x.MainModuleId == id).ToList();
            else
                model.List = applicationDbContext.SubModule.ToList();
            return View(model);


        }
        [AllowAnonymous]
        public ActionResult Detail(int? id)
        {
            ViewBag.Message = "Your contact page.";


            var data = applicationDbContext.SubModule.Where(x => x.Id == id).FirstOrDefault();
            return View(data);


        }
        [AllowAnonymous]

        public ActionResult ViewImage(int? id)
        {

            Notice model = new Notice();
            model = applicationDbContext.Notice.Find(id);

            return PartialView("_ShowImage", model);
        }
        [AllowAnonymous]
        public ActionResult ShowEmployee(int? id)
        {
            HomePageViewModel model = new HomePageViewModel();
            model.employeeSetup = new EmployeeSetup();
            model.employeeSetup = applicationDbContext.EmployeeSetup.Find(id);
            return PartialView("_ShowEmployee", model);
        }
        //public bool FileExist(string path)
        //{
        //    if (System.IO.File.Exists(path))
        //    {
        //        return true;
        //    }
        //    else return false;

        //}
        //public int GetAnotherid(int id)
        //{
        //    int Maxid = applicationDbContext.Video.Max(x => x.VideoId);
        //    for (int i = id + 1; i <= Maxid; i++)
        //    {
        //        var video = applicationDbContext.Video.Find(i);
        //        var path = "~/UploadedVideos/" + video.VideoData;
        //        if (FileExist(path))
        //        {
        //            return i;
        //        }
        //    }
        //    return id;
        //}
        [AllowAnonymous]
        public ActionResult ShowVideos(int id)
        {
            HomePageViewModel model = new HomePageViewModel();
            model.video = new Video();
            int Maxid = applicationDbContext.Video.Max(x => x.VideoId);
            if (id > Maxid)
            {
                model.video = applicationDbContext.Video.FirstOrDefault();
                return PartialView("_ShowVideo", model);

            }

            var data= applicationDbContext.Video.Find(id);
            if (data == null)
            {
                model.video = applicationDbContext.Video.Where(x => x.VideoId > id).Where(x => x.VideoUrl != null).FirstOrDefault();
                return PartialView("_ShowVideo", model);
            }
            else
            model.video = applicationDbContext.Video.Find(id);

            return PartialView("_ShowVideo", model);
            //var path = "~/UploadedVideos/" + model.video.VideoData;
            //if (FileExist(path))
            //{
            //    return PartialView("_ShowVideo", model);
            //}
            //else
            //{
            //    var nextid = GetAnotherid(id);
            //    model.video = applicationDbContext.Video.Find(nextid);
            //    return PartialView("_ShowVideo", model);
            //}






        }


    }
}