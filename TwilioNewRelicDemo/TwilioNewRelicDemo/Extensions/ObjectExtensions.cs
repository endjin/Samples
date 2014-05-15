namespace TwilioNewRelicDemo.Extensions
{
    #region Using Directives

    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using RestSharp.Extensions;

    #endregion

    public static class ObjectExtensions
    {
        public static Dictionary<string, string> ToParametersDictionary(
                this object parameters,
                string prefix = null,
                bool includeNullValues = true)
        {
            var args = new Dictionary<string, string>();
            if (parameters == null)
            {
                return args;
            }

            var nullValue = includeNullValues ? "null" : null;

            var json = JsonConvert.SerializeObject(parameters, Formatting.None);
            var jo = JObject.Parse(json);

            foreach (var prop in jo.Properties())
            {
                var name = string.IsNullOrEmpty(prefix) ? prop.Name : string.Concat(prefix, "_", prop.Name);
                var value = (prop.Value.Type == JTokenType.Array || prop.Value.Type == JTokenType.Object)
                        ? prop.Value.ToString(Formatting.None)
                        : ((JValue)prop.Value).Value;

                args.Add(name, value == null ? nullValue : value.ToString());
            }

            return args;
        }
    }
}