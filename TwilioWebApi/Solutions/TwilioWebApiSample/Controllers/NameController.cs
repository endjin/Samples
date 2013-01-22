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

    public class NameController : ApiController
    {
        public HttpResponseMessage Post(VoiceRequest request, string userId)
        {
            var response = new TwilioResponse();

            var usernames = new Dictionary<string, string>
                                {
                                    { "12345", "Tom" }, 
                                    { "23456", "Dick" }, 
                                    { "34567", "Harry" }
                                };

            var username = usernames[userId];

            response.Say(string.Format("Hello {0}", username));
            response.Pause(5);
            response.Hangup();

            return this.Request.CreateResponse(HttpStatusCode.OK, response.Element, new XmlMediaTypeFormatter());
        }
    }
}