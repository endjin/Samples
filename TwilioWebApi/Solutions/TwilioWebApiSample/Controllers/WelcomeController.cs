namespace TwilioWebApiSample.Controllers
{
    #region Using Directives

    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Web.Http;

    using Twilio.Mvc;
    using Twilio.TwiML;

    using TwilioWebApiSample.Attributes;

    #endregion

    [ValidateTwilioRequest("https://kwd-endjin.fwd.wf/api/welcome")]
    public class WelcomeController : ApiController
    {
        public HttpResponseMessage Post(VoiceRequest request)
        {
            var response = new TwilioResponse();

            response.Say("Welcome to this Twilio demo app. Please enter your 5 digit ID.");
            response.Gather(new { numDigits = 5, action = string.Format("/api/Authenticate") });

            return this.Request.CreateResponse(HttpStatusCode.OK, response.Element, new XmlMediaTypeFormatter());
        }
    }
}