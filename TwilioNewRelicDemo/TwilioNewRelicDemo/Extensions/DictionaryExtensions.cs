namespace TwilioNewRelicDemo.Extensions
{
    using System.Collections.Generic;
    using System.Web.Http.Controllers;

    using Twilio.Mvc;

    public static class DictionaryExtensions
    {
        public static Dictionary<string, string> AddHttpActionContextParameters(
                this Dictionary<string, string> dictionary,
                HttpActionContext context)
        {
            foreach (var actionArgument in context.ActionArguments)
            {
                if (actionArgument.Value is VoiceRequest)
                {
                    continue;
                }

                dictionary.Add(
                               actionArgument.Key,
                               actionArgument.Value != null ? actionArgument.Value.ToString() : "null");
            }

            return dictionary;
        }
    }
}