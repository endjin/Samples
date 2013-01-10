namespace WebApiIntegrationTestsSample.Storage
{
    using System;

    using WebApiIntegrationTestsSample.Domain;

    public class RecordsRepository
    {
        public Record GetRecord(int id)
        {
            return new Record { Id = id, Content = "Hello!", Name = "Mike" };
        }

        public int StoreRecord(Record record)
        {
            var random = new Random();

            var id = random.Next(1, 100);

            // Save some record and return its ID

            return id;
        }
    }
}