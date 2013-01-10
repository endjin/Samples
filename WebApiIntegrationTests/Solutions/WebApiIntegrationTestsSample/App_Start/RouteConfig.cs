namespace WebApiIntegrationTestsSample.App_Start
{
    #region Using Directives

    using System.Web.Mvc;
    using System.Web.Routing;

    #endregion

    public class RouteConfig
    {
        public static void RegisterRoutes()
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");
            RouteTable.Routes.IgnoreRoute("{resource}.asmx/{*pathInfo}");

            RouteTable.Routes.AddHttpRoutes();

            RouteTable.Routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}