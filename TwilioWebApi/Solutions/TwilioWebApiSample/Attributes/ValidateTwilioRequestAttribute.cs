namespace TwilioWebApiSample.Attributes
{
    #region Using Directives

    using System;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    #endregion

    public class ValidateTwilioRequestAttribute : ActionFilterAttribute
    {
        private readonly string authToken;
        private readonly string urlOverride;

        public ValidateTwilioRequestAttribute()
        {
            this.authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
        }

        public ValidateTwilioRequestAttribute(string urlOverride)
        {
            this.authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
            this.urlOverride = urlOverride;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!IsValidRequest(actionContext, this.authToken, this.urlOverride))
            {
                var response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Forbidden
                };
                throw new HttpResponseException(response);
            }

            base.OnActionExecuting(actionContext);
        }

        private static bool IsValidRequest(HttpActionContext context, string authToken, string urlOverride = null)
        {
            var value = new StringBuilder();
            
            // Take the host URL from the request, or use the URL override if there is one
            var fullUrl = string.IsNullOrEmpty(urlOverride) ? context.Request.RequestUri.ToString() : urlOverride;

            value.Append(fullUrl);

            var request = HttpContext.Current.Request;

            // If POST request, concatenate the key-value pairs in the request
            if (context.Request.Method == HttpMethod.Post)
            {
                var sortedKeys = request.Form.AllKeys.OrderBy(k => k, StringComparer.Ordinal).ToList();
                foreach (var key in sortedKeys)
                {
                    value.Append(key);
                    value.Append(request.Form[key]);
                }
            }

            // Create signature using AuthToken as key
            var sha1 = new HMACSHA1(Encoding.UTF8.GetBytes(authToken));
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(value.ToString()));
            var encoded = Convert.ToBase64String(hash);

            var sig = request.Headers["X-Twilio-Signature"];

            // Compare our signatures
            return sig == encoded;
        }
    }
}