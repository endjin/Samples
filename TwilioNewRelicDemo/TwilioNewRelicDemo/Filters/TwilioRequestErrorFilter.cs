namespace TwilioNewRelicDemo.Filters
{
    #region Using Directives

    using System.Linq;
    using System.Web.Http.Filters;

    using Twilio.Mvc;

    using TwilioNewRelicDemo.Exceptions;
    using TwilioNewRelicDemo.Extensions;
    using TwilioNewRelicDemo.Logging;

    #endregion

    public class TwilioRequestErrorFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            // Only apply to web api requests than contain a Twilio Voice Request
            var voiceRequest =
                    context.ActionContext.ActionArguments.Select(kvp => kvp.Value)
                            .OfType<VoiceRequest>()
                            .FirstOrDefault();

            if (voiceRequest == null)
            {
                return;
            }

            var customException = context.Exception as CustomException;
            var errorAction = customException == null ? ErrorAction.Escalate : customException.ErrorAction;

            var parameters = 
                context.ActionContext.ActionArguments.ToRequestParameters();

            NewRelicLogger.LogError(context.Exception, errorAction, parameters);
        }
    }
}