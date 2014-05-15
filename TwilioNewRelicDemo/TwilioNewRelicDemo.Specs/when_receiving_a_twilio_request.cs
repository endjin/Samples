namespace TwilioNewRelicDemo.Specs
{
    #region Using Directives

    using System.Net.Http;
    using System.Web.Http;

    using NUnit.Framework;

    using Twilio.Mvc;

    #endregion

    [TestFixture]
    public class when_receiving_a_twilio_request
    {
        protected const string ValidPin = "123";
        protected const string InvalidPin = "098";

        public when_receiving_a_twilio_request()
        {
            this.establish_context();
            this.because_of();
        }

        protected virtual void because_of()
        {
        }

        protected virtual void establish_context()
        {
        }

        protected HttpResponseMessage Post(string uri, VoiceRequest request)
        {
            using (var client = CreateHttpClient())
            {
                return client.PostAsJsonAsync(uri, request).Result;
            }
        }

        private static HttpClient CreateHttpClient()
        {
            var config = new HttpConfiguration();

            WebApiConfig.Register(config);
            FilterConfig.Register(config);

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            var server = new HttpServer(config);
            return new HttpClient(server);
        }
    }
}