namespace TwilioNewRelicDemo.Controllers
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Net.Http;

    using Twilio.Mvc;
    using Twilio.TwiML;

    using TwilioNewRelicDemo.Exceptions;
    using TwilioNewRelicDemo.Services;

    #endregion

    public class AuthenticateController : TwilioApiController
    {
        public HttpResponseMessage Post(VoiceRequest request)
        {
            var pin = request.Digits;

            var authenticated = AuthenticationService.Authenticate(pin);

            if (!authenticated)
            {
                throw new InvalidPinException(pin);
            }

            var response = new TwilioResponse();
            response.Say("Pin code is valid.");
            response.Redirect(string.Format("/api/Name?pin={0}", pin));

            return this.TwiMLResponse(response);
        }
    }
}