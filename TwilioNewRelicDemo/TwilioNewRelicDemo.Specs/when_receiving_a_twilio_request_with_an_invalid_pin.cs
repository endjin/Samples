namespace TwilioNewRelicDemo.Specs
{
    #region Using Directives

    using System.Diagnostics;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Hosting;

    using NUnit.Framework;

    using Twilio.Mvc;

    using TwilioNewRelicDemo.Controllers;
    using TwilioNewRelicDemo.Specs.Helpers;

    #endregion

    public class when_receiving_a_twilio_request_with_an_invalid_pin : when_receiving_a_twilio_request
    {
        private VoiceRequest request;
        private HttpResponseMessage response;
        private AuthenticateController sut;
        private TestTraceListener traceListener;

        protected override void establish_context()
        {
            this.traceListener = new TestTraceListener();
            Trace.Listeners.Add(this.traceListener);

            this.request = TestData.CreateTestVoiceRequest(InvalidPin);
            this.sut = new AuthenticateController() { Request = new HttpRequestMessage() };

            this.sut.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }

        protected override void because_of()
        {
            this.response = this.Post("http://test/api/authenticate", this.request);
        }

        [Test]
        public void it_should_return_an_unsuccessful_http_status_code()
        {
            Assert.False(this.response.IsSuccessStatusCode);
        }

        [Test]
        public void it_should_log_the_error()
        {
            Assert.True(this.traceListener.Contains("Logging to NewRelic"));
        }
    }
}