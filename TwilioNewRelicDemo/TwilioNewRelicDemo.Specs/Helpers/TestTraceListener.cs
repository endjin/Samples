namespace TwilioNewRelicDemo.Specs.Helpers
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    #endregion


    public class TestTraceListener : TraceListener
    {
        private readonly Stack<string> messages = new Stack<string>();

        public bool Contains(string partialMessage)
        {
            return this.messages.Any(msg => msg.Contains(partialMessage));
        }

        public override void Write(string message)
        {
            this.messages.Push(message);
        }

        public override void WriteLine(string message)
        {
            this.messages.Push(message);
        }
    }
}