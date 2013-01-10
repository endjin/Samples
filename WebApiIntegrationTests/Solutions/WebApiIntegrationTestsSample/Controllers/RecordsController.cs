namespace WebApiIntegrationTestsSample.Controllers
{
    #region Using Directives

    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using WebApiIntegrationTestsSample.Domain;
    using WebApiIntegrationTestsSample.Storage;

    #endregion

    public class RecordsController : ApiController
    {
        private readonly RecordsRepository db;

        public RecordsController()
        {
            this.db = new RecordsRepository();
        }

        public HttpResponseMessage Get(int id)
        {
            var record = this.db.GetRecord(id);

            return this.Request.CreateResponse(HttpStatusCode.OK, record);
        }

        public HttpResponseMessage Post(Record record)
        {
            var recordId = this.db.StoreRecord(record);

            return this.Request.CreateResponse(HttpStatusCode.Created, recordId);
        }
    }
}