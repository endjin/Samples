namespace WebApiIntegrationTestsSample.App_Start
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Routing;

    public static class WebApiConfig
    {
        public static void AddHttpRoutes(this HttpRouteCollection routeCollection)
        {
            var routes = GetRoutes();
            routes.ForEach(route => routeCollection.MapHttpRoute(route.Name, route.Template, route.Defaults));
        }

        public static void AddHttpRoutes(this RouteCollection routeCollection)
        {
            var routes = GetRoutes();
            routes.ForEach(route => routeCollection.MapHttpRoute(route.Name, route.Template, route.Defaults));
        }

        private static List<Route> GetRoutes()
        {
            return new List<Route>
                       {
                           new Route(
                               "DefaultApi", 
                               "api/{controller}/{id}", 
                               new { id = RouteParameter.Optional })
                       };
        }

        private class Route
        {
            public Route(string name, string template, object defaults)
            {
                this.Name = name;
                this.Template = template;
                this.Defaults = defaults;
            }

            public object Defaults { get; set; }

            public string Name { get; set; }

            public string Template { get; set; }
        }
    }
}