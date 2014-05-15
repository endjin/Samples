namespace TwilioNewRelicDemo
{
    #region Using Directives

    using System.Web.Http;

    using TwilioNewRelicDemo.Filters;

    #endregion

    public static class FilterConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new TwilioRequestErrorFilter());
        }
    }
}