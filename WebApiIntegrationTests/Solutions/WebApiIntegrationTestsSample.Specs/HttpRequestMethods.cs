namespace WebApiIntegrationTestsSample.Specs
{
    #region Using Directives

    using System;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;

    #endregion

    public static class HttpRequestMethods
    {
        public static HttpRequestMessage CreateRequest<T>(string uri, string mthv, HttpMethod method, T content, MediaTypeFormatter formatter) where T : class
        {
            HttpRequestMessage request = CreateRequest(uri, mthv, method);
            request.Content = new ObjectContent<T>(content, formatter);

            return request;
        }

        public static HttpRequestMessage CreateRequest(string uri, string mthv, HttpMethod method)
        {
            var request = new HttpRequestMessage { RequestUri = new Uri(uri) };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mthv));
            request.Method = method;

            return request;
        }
    }
}