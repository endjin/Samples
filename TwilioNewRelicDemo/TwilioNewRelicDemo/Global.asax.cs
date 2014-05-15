namespace TwilioNewRelicDemo
{
    #region Using Directives

    using System.Web;
    using System.Web.Http;

    #endregion

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(FilterConfig.Register);
        }
    }
}