namespace TwilioNewRelicDemo.Specs
{
    #region Using Directives

    using System.Net.Http;

    using NUnit.Framework;

    using Twilio.Mvc;

    using TwilioNewRelicDemo.Specs.Helpers;

    #endregion
    public class when_receiving_a_twilio_request_with_a_valid_pin : when_receiving_a_twilio_request
    {
        private VoiceRequest request;
        private HttpResponseMessage response;

        protected override void establish_context()
        {
            this.request = TestData.CreateTestVoiceRequest(ValidPin);
        }

        protected override void because_of()
        {
            this.response = Post("http://test/api/authenticate", this.request);
        }

        [Test]
        public void it_should_return_a_valid_twilio_response()
        {
            Assert.True(this.response.IsSuccessStatusCode);
        }
    }
}