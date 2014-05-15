namespace TwilioNewRelicDemo.Exceptions
{
    #region Using Directives

    using TwilioNewRelicDemo.Logging;

    #endregion

    public class InvalidPinException : CustomException
    {
        public InvalidPinException(string pin)
                : base(ToMessage(pin))
        {
        }

        public override ErrorAction ErrorAction
        {
            get
            {
                return ErrorAction.Ignore;
            }
        }

        private static string ToMessage(string pin)
        {
            return string.Format("Pin invalid: {0}", pin);
        }
    }
}