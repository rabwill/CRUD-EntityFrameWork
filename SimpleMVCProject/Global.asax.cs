using SimpleMVCProject.App_Start;
using System.Web.Http;


namespace SimpleMVCProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Web Api Routing
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Json formatter
            JsonFormatter.Configure();

            // Simple injector
            App_Start.SimpleInjector.Initialize();
        }
    }
}
