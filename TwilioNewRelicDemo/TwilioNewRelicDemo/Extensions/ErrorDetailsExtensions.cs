namespace TwilioNewRelicDemo.Extensions
{
    using TwilioNewRelicDemo.Logging;

    public static class ErrorDetailsExtensions
    {
        public static string ExtractErrorMessage(this ErrorDetails details)
        {
            return details.Error == null ? (details.ErrorMessage ?? "unknown") : details.Error.Message;
        }

        public static string ExtractStackTrace(this ErrorDetails details)
        {
            return details.Error == null ? (details.StackTrace ?? string.Empty) : details.Error.StackTrace;
        }
    }
}