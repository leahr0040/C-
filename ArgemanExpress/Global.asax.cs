using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ArgemanExpress
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Bl.TaskBL.RunPrepareDaily(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 19, 40, 0));//קריאה לפונקציה שתעיר פונקציה כל יום ב 6:00
           Bl.TaskBL.setMonthly(DateTime.Now.AddMinutes(5));
         Bl.RentalBL.setYearly(DateTime.Now.AddMinutes(20));
            //Console.ReadLine();
        }
    }
}
