namespace TwilioNewRelicDemo.Controllers
{
    #region Using Directives

    using System.Net.Http;

    using Twilio.Mvc;
    using Twilio.TwiML;

    using TwilioNewRelicDemo.Services;

    #endregion

    public class UserController : TwilioApiController
    {
        public HttpResponseMessage Post(VoiceRequest request, string pin)
        {
            var user = AuthenticationService.GetUser(pin);

            var response = new TwilioResponse();
            response.Say(string.Format("Hello {0}", user.Name));
            response.Pause(2);
            response.Hangup();

            return this.TwiMLResponse(response);
        }
    }
}