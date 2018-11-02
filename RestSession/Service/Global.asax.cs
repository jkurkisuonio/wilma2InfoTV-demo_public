using System;
using System.Web;
using System.Web.Routing;
using System.ServiceModel.Activation;
using Service.Services;

namespace Service
{
    public class Global : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.Add(new ServiceRoute("",
                new WebServiceHostFactory(),
                typeof(StudentService)));
        }

        void Application_End(object sender, EventArgs e) { }
        void Application_Error(object sender, EventArgs e) { }
        void Session_Start(object sender, EventArgs e) { }
        void Session_End(object sender, EventArgs e) { }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Disable all caching for simplicity
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
        }
    }
}
