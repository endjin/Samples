namespace WebApiIntegrationTestsSample
{
    #region Using Directives

    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;

    using WebApiIntegrationTestsSample.App_Start;

    #endregion

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RouteConfig.RegisterRoutes();
        }
    }
}