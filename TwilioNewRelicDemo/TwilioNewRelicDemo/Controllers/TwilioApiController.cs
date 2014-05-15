namespace TwilioNewRelicDemo.Controllers
{
    #region Using Directives

    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Web.Http;

    using Twilio.TwiML;

    #endregion

    public abstract class TwilioApiController : ApiController
    {
        protected HttpResponseMessage TwiMLResponse(TwilioResponse response)
        {
            return this.Request.CreateResponse(HttpStatusCode.OK, response.Element, new XmlMediaTypeFormatter());
        }
    }
}