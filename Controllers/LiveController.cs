using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpDesk.Controllers
{
    public class LiveController : Controller
    {
        // GET: Live
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult _Live()
        {
            return View();
        }
    }
}