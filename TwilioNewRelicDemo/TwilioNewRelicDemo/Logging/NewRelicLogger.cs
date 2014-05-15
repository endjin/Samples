namespace TwilioNewRelicDemo.Logging
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using NewRelic.Api.Agent;

    using TwilioNewRelicDemo.Extensions;

    #endregion

    public class NewRelicLogger
    {
        public static void LogError(
                Exception ex,
                ErrorAction actionTaken,
                object parameters)
        {
            try
            {
                var errorDetails = Create(
                        ex,
                        ex.Message,
                        ex.StackTrace,
                        actionTaken,
                        parameters);

                LogToNewRelic(errorDetails);
            }
            catch (Exception exception)
            {
                Trace.TraceError(
                        string.Format("[{0}] Error: Logging to NewRelic Failed. {1}", "NewRelicLogger", exception.Message));
            }
        }

        private static ErrorDetails Create(
                Exception ex,
                string errorMessage,
                string stackTrace,
                ErrorAction actionTaken,
                object parameters)
        {
            var args = parameters as Dictionary<string, string> ?? parameters.ToParametersDictionary();

            return new ErrorDetails
            {
                ActionTaken = actionTaken,
                Args = args,
                Error = ex,
                ErrorMessage = errorMessage,
                StackTrace = stackTrace,
                OccurrenceTimeStamp = DateTime.UtcNow
            };
        }

        private static void LogToNewRelic(ErrorDetails details)
        {
            Trace.TraceError(
                             string.Format(
                                           "Error: {0}",
                                           details.Error.Message));

            var errorData = new Dictionary<string, string>(details.Args)
            {
                { "ActionTaken", details.ActionTaken.ToString() },
                { "OccurrenceTimeStamp", details.OccurrenceTimeStamp.ToString("O") },
                { "Error", details.ExtractErrorMessage() },
                { "StackTrace", details.ExtractStackTrace() }
            };

            Trace.TraceInformation("Logging to NewRelic - Error: {0}", details.Error);

            // Log error to New Relic using API agent
            NewRelic.NoticeError(details.Error, errorData);
        }
    }
}