namespace TwilioNewRelicDemo.Specs.Helpers
{
    #region Using Directives

    using Twilio.Mvc;

    #endregion

    public static class TestData
    {
        public static VoiceRequest CreateTestVoiceRequest(string digits)
        {
            return new VoiceRequest
            {
                AccountSid = "accountSid",
                ApiVersion = "apiVersion",
                CallSid = "callSid",
                CallStatus = "callStatus",
                CallerName = "callerName",
                DialCallDuration = "dialCallDuration",
                DialCallSid = "dialCallSid",
                DialCallStatus = "dialCallStatus",
                Digits = digits,
                Direction = "direction",
                ForwardedFrom = "forwardedFrom",
                From = "from",
                FromCity = "fromCity",
                FromCountry = "fromCountry",
                FromState = "fromState",
                FromZip = "fromZip",
                RecordingDuration = "recordingDuration",
                RecordingSid = "recordingSid",
                RecordingUrl = "recordingUrl",
                To = "to",
                ToCity = "toCity",
                ToCountry = "toCountry",
                ToState = "toState",
                ToZip = "toZip",
                TranscriptionSid = "transcripotionSid",
                TranscriptionStatus = "transcriptionStatus",
                TranscriptionText = "transcriptionText",
                TranscriptionUrl = "transcriptionUrl"
            };
        }
    }
}