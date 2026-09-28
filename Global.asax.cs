using AutoMapper;
using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HelpDesk
{


    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_BeginRequest()
        {
          
            //if (Request.IsSecureConnection)
            //{
            //    Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload");
            //}

            //if (!Context.Request.IsSecureConnection)
            //{
            //    string url = Context.Request.Url.ToString().Replace("http:", "https:");
            //    Response.Redirect(url);
            //}
         
        }

        protected void Application_PostAuthenticateRequest()
        {
            //if (HttpContext.Current.Request.IsSecureConnection)
            //{
            //    Response.Cookies[".ASPXAUTH"].Secure = true;
            //}
        }

        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            //HttpContext.Current.Response.Headers.Set("Server", string.Empty);
            //HttpContext.Current.Response.Headers.Remove("X-AspNetWebPages-Version");
            //HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
            //HttpContext.Current.Response.Headers.Remove("X-Powered-By");
            //HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
        }


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //Mapper.CreateMap<AppUser, AppUserEntry>().ForMember((AppUserEntry dest) => dest.RoleName, delegate (IMemberConfigurationExpression<AppUser> opts)
            //{
            //    opts.MapFrom<string>((AppUser src) => src.UserRole.Name);
            //});
            //Mapper.CreateMap<AppUserEntry, AppUser>();
            MvcHandler.DisableMvcResponseHeader = true;


        }



        //protected void Application_Error()
        //{
        //    Exception exception = Server.GetLastError();
        //    Response.Clear();
        //    Server.ClearError();

        //    var httpException = exception as HttpException;
        //    if (httpException != null && httpException.GetHttpCode() == 404)
        //    {
        //        Response.Redirect("~/Error/NotFound");

        //    }
        //    else
        //    {
        //        Response.Redirect("~/Error/ServerError");
        //    }




        //}


        //protected void Application_EndRequest()
        //{



        //    var headers = HttpContext.Current.Response.Headers;
        //    var cookies = headers.GetValues("Set-Cookie");

        //    if (cookies != null)
        //    {
        //        headers.Remove("Set-Cookie");

        //        foreach (var cookie in cookies)
        //        {
        //            string updatedCookie = cookie;

        //            if (!cookie.Contains("SameSite"))
        //            {
        //                updatedCookie += "; SameSite=Lax"; // Or Strict / None (if you use Secure)
        //            }

        //            headers.Add("Set-Cookie", updatedCookie);
        //        }
        //    }






        //}

    }

}
