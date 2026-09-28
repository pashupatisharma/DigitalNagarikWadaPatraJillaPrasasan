using AutoMapper;
using HelpDesk.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Linq;

using System.Web.Mvc;
using System.Web.Security;
using System.Web;


namespace HelpDesk
{
    public static class Utility
    {
        public static List<SelectListItem> GetRolesList()
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            List<SelectListItem> list = new List<SelectListItem>();
            var listll = applicationDbContext.Roles.ToList();

            foreach (var item in listll)
            {
                list.Add(new SelectListItem() { Value = item.Name.ToString(), Text = item.Name });
            }
            return list;
        }


        public static Offices officeDetail()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var of = db.Offices.FirstOrDefault();
            return of;
        }

        public static byte[] ReturnByte(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return null;
            }
            byte[] buffer = null;

            if (file != null)
            {
                buffer = new byte[file.InputStream.Length];
                file.InputStream.Read(buffer, 0, buffer.Length);
            }
            return buffer;
        }


        public static IEnumerable<SelectListItem> GetGender()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem() { Value = null, Text = "छान्नुहोस्" });
            list.Add(new SelectListItem() { Value = "1", Text = "पुरुष" });
            list.Add(new SelectListItem() { Value = "2", Text = "महिला" });
            list.Add(new SelectListItem() { Value = "3", Text = "अन्य" });
            return list;
        }

        public static IEnumerable<SelectListItem> GetEmpTypeList()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var lists = new List<SelectListItem>();
            var subjectslists = db.EmployeeTypeSetup.ToList();
            lists.Add(new SelectListItem() { Value = null, Text = "छान्नुहोस्" });
            foreach (var item in subjectslists)
            {
                lists.Add(new SelectListItem() { Value = item.EmployeeTypeID.ToString(), Text = item.EmployeeTypeName + "[" + item.EmployeeTypeNameEng + "]" });
            }
            return lists;
        }

        public static IEnumerable<SelectListItem> GetPostList()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var lists = new List<SelectListItem>();
            var subjectslists = db.PostSetup.ToList();
            lists.Add(new SelectListItem() { Value = null, Text = "छान्नुहोस्" });
            foreach (var item in subjectslists)
            {
                lists.Add(new SelectListItem() { Value = item.PostId.ToString(), Text = item.PostTitle + "[" + item.PostTitleEng + "]" });
            }
            return lists;
        }

        public static string GetPostNameById(int? Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var data = db.PostSetup.Where(x => x.PostId == Id).FirstOrDefault();
            var pstName = "N/A";
            if (data != null)
            {
                pstName = data.PostTitle + "[" + data.PostTitleEng + "]";
            }
            return pstName;
        }




    }
}