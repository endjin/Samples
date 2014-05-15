namespace TwilioNewRelicDemo.Logging
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    public class ErrorDetails
    {
        public ErrorAction ActionTaken
        {
            get;
            set;
        }

        public Dictionary<string, string> Args
        {
            get;
            set;
        }

        public Exception Error
        {
            get;
            set;
        }

        public string ErrorMessage
        {
            get;
            set;
        }

        public DateTime OccurrenceTimeStamp
        {
            get;
            set;
        }

        public string StackTrace
        {
            get;
            set;
        }
    }
}