namespace TwilioNewRelicDemo.Specs
{
    #region Using Directives

    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Hosting;

    using NUnit.Framework;

    using Twilio.Mvc;

    using TwilioNewRelicDemo.Controllers;
    using TwilioNewRelicDemo.Specs.Helpers;

    #endregion
    public class when_receiving_a_twilio_request_with_a_valid_pin : when_receiving_a_twilio_request
    {
        private VoiceRequest request;
        private AuthenticateController sut;
        private HttpResponseMessage response;

        protected override void establish_context()
        {
            this.request = TestData.CreateTestVoiceRequest(ValidPin);
            this.sut = new AuthenticateController()
            {
                Request = new HttpRequestMessage()
            };

            this.sut.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }

        protected override void because_of()
        {
            this.response = this.Post("http://test/api/authenticate", this.request);
        }

        [Test]
        public void it_should_return_a_valid_twilio_response()
        {
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}