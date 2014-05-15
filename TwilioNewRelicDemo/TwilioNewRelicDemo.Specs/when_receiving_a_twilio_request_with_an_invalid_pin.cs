namespace TwilioNewRelicDemo.Specs
{
    #region Using Directives

    using System.Diagnostics;
    using System.Net.Http;

    using NUnit.Framework;

    using Twilio.Mvc;

    using TwilioNewRelicDemo.Specs.Helpers;

    #endregion

    public class when_receiving_a_twilio_request_with_an_invalid_pin : when_receiving_a_twilio_request
    {
        private VoiceRequest request;
        private HttpResponseMessage response;
        private TestTraceListener traceListener;

        protected override void establish_context()
        {
            this.traceListener = new TestTraceListener();
            Trace.Listeners.Add(this.traceListener);

            this.request = TestData.CreateTestVoiceRequest(InvalidPin);
        }

        protected override void because_of()
        {
            this.response = Post("http://test/api/authenticate", this.request);
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