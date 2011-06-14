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
            return new Guid(Membership.GetUser().ProviderUserKey.ToString());
        }
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

    //        routes.MapRoute(
    //            "CreateFeedback", // Route name
    //            "Site/{name}/CreateFeedback", // URL with parameters
    //            new { controller = "Feedback", action = "Create" } // Parameter defaults
    //            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
                );
        }

        protected void Application_Start()
        {
            Database.SetInitializer(new FeedbackServiceContextInitializer());


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

            context.FeedbackTypes.Add(new FeedbackType { TypeName = "Пожелание" });
            context.FeedbackTypes.Add(new FeedbackType { TypeName = "Вопрос" });
            context.FeedbackTypes.Add(new FeedbackType { TypeName = "Отчет об ошибке" });
            context.SaveChanges();
        }
    }
}