namespace WebApiIntegrationTestsSample.Specs
{
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Threading;
    using System.Web.Http;
    using NUnit.Framework;
    using WebApiIntegrationTestsSample.App_Start;
    using WebApiIntegrationTestsSample.Domain;

    [TestFixture]
    public class RecordsControllerSpecs
    {
        private HttpClient client;

        [SetUp]
        public void HttpClientSetup()
        {
            var config = new HttpConfiguration();

            config.Routes.AddHttpRoutes();
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            var server = new HttpServer(config);
            this.client = new HttpClient(server);
            
        }

        [TestCase]
        public void GetRecordTest()
        {
            var url = "http://WebApiTests.com/api/records/1";

            var request = HttpRequestMethods.CreateRequest(url, "application/json", HttpMethod.Get);

            HttpResponseMessage response = this.client.SendAsync(request, new CancellationTokenSource().Token).Result;

            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(response.Content);
        }

        [TestCase]
        public void PostRecordTest()
        {
            var url = "http://WebApiTests.com/api/records";

            var record = new Record { Name = "Mike", Content = "Hello!" };

            var request = HttpRequestMethods.CreateRequest(
                url, "application/json", HttpMethod.Post, record, new JsonMediaTypeFormatter());

            HttpResponseMessage response = this.client.SendAsync(request, new CancellationTokenSource().Token).Result;

            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(response.Content);
        }

    }
}
