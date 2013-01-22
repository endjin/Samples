namespace TwilioWebApiSample.App_Start
{
    #region Using Directives

    using System.Web.Http;

    using TwilioWebApiSample.Controllers;

    #endregion

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi", 
                routeTemplate: "api/{controller}/{id}", 
                defaults: new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "NameController", 
                routeTemplate: "api/Name/{userId}",
                defaults: new { controller = "Name" });
        }
    }
}