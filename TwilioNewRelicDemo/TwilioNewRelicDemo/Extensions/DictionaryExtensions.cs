namespace TwilioNewRelicDemo.Extensions
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;

    using Twilio.Mvc;

    #endregion

    public static class DictionaryExtensions
    {
        public static Dictionary<string, string> ToRequestParameters(this Dictionary<string, object> args)
        {
            var voiceRequest = args.Select(kvp => kvp.Value).OfType<VoiceRequest>().FirstOrDefault();

            var parameters = voiceRequest.ToParametersDictionary();

            var otherParameters = args.Where(arg => !(arg.Value is VoiceRequest));
            foreach (var parameter in otherParameters)
            {
                parameters.Add(parameter.Key, parameter.Value != null ? parameter.Value.ToString() : "null");
            }

            return parameters;
        }
    }
}