namespace TwilioNewRelicDemo.Exceptions
{
    #region Using Directives

    using System;

    using TwilioNewRelicDemo.Logging;

    #endregion

    public abstract class CustomException : Exception
    {
        protected CustomException(string message)
                : base(message)
        {
        }

        public abstract ErrorAction ErrorAction
        {
            get;
        }
    }
}