namespace TwilioWebApiSample.Controllers
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Web.Http;

    using Twilio.Mvc;
    using Twilio.TwiML;

    #endregion

    public class AuthenticateController : ApiController
    {
        public HttpResponseMessage Post(VoiceRequest request)
        {
            var response = new TwilioResponse();

            var validIds = new List<string> { "12345", "23456", "34567" };
            var userId = request.Digits;
            var authenticated = validIds.Contains(userId);

            if (!authenticated)
            {
                response.Say("You entered an invalid ID. Goodbye.");
                response.Hangup();
            }
            else
            {
                response.Say("ID is valid.");
                response.Redirect(string.Format("/api/Name?userId={0}", userId));
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, response.Element, new XmlMediaTypeFormatter());
        }
    }
}