using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using FeedbackService.Models;
using System.Web.Security;
using System.Web.Mvc.Html;
using System.Net;

namespace FeedbackService
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public static class Constants
    {
        public static string RootRoleName = "admin";
        public static string ThisSiteGuid = "00000000-0000-0000-0000-000000000001";//Dns.GetHostName();
    }

    public static class Helper
    {
        public static Guid UserGuid()
        {
            MembershipUser user = Membership.GetUser();
            if (user == null)
                return new Guid("00000000-0000-0000-0000-000000000000");

            return new Guid(Membership.GetUser().ProviderUserKey.ToString());
        }

        public static string CaptchaPublicKey = "6LfmOMUSAAAAAKld6kjnViZMtF-2qWrb_SO5-pDa";
        public static string CaptchaPrivateKey = "6LfmOMUSAAAAAABR2AqhSikXKQhpUU81tfma6hE1";
    }

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //        routes.MapRoute(
            //"SiteDetails", // Route name
            //"Site/{name}", // URL with parameters
            //new { controller = "Site", action = "Details" } // Parameter defaults
            //);

            //        routes.MapRoute(
            //            "SiteDetails", // Route name
            //            "Site/{name}", // URL with parameters
            //            new { controller = "Site", action = "Details" } // Parameter defaults
            //            );

            //routes.MapRoute(
            //    "Vote", // Route name
            //    "Feedback/{action}/{id}/Vote", // URL with parameters
            //    new { controller = "Feedback", action = "Vote" } // Parameter defaults
            //    );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
                );
        }

        protected void Application_Start()
        {
            Database.SetInitializer(new FeedbackServiceContextInitializer());

            Recaptcha.RecaptchaControlMvc.PrivateKey = Helper.CaptchaPrivateKey;
            Recaptcha.RecaptchaControlMvc.PublicKey = Helper.CaptchaPublicKey;

            //Database.SetInitializer(new CreateDatabaseIfNotExists<FeedbackServiceContext>());

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }

    public class FeedbackServiceContextInitializer : DropCreateDatabaseIfModelChanges<FeedbackServiceContext>
    {
        protected override void Seed(FeedbackServiceContext context)
        {
            base.Seed(context);

            foreach (MembershipUser user in Membership.GetAllUsers())
            {
                context.Clients.Add(new Client
                {
                    ClientId = new Guid(user.ProviderUserKey.ToString()),
                    ClientName = user.UserName,
                    Email = user.Email
                });
            }
            context.SaveChanges();
        }
    }
}