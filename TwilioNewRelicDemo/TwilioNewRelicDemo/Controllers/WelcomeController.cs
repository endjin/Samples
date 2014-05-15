namespace TwilioNewRelicDemo.Controllers
{
    #region Using Directives

    using System.Net.Http;

    using Twilio.Mvc;
    using Twilio.TwiML;

    #endregion

    public class WelcomeController : TwilioApiController
    {
        public HttpResponseMessage Post(VoiceRequest request)
        {
            var response = new TwilioResponse();
            response.Say("Welcome to this Twilio demo app. Please enter your 3 digit pin code.");
            response.Gather(new { numDigits = 3, action = string.Format("/api/Authenticate") });

            return this.TwiMLResponse(response);
        }
    }
}