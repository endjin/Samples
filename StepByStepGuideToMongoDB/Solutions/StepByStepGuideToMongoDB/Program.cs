namespace StepByStepGuideToMongoDB
{
    #region Using Directives

    using System;

    using StepByStepGuideToMongoDB.Contracts.Tasks;
    using StepByStepGuideToMongoDB.Domain;
    using StepByStepGuideToMongoDB.Framework.Infrastructure.Repositories;
    using StepByStepGuideToMongoDB.Infrastructure.DataSources;
    using StepByStepGuideToMongoDB.Tasks;

    #endregion

    public static class Program
    {
        private static void Main(string[] args)
        {
            IClubTasks clubTasks = new ClubTasks(new RemoteClubDataSource(), new ReadWriteRepository<Club>());

            // Retrieve Clubs from the XML Data Fields
            // Using LINQ to XML to generate POCO from the feed
            var allClubs = clubTasks.RetrieveClubsFromRemoteDataSource();

            // Add each club to MongoDB
            clubTasks.AddToLocalRepository(allClubs);

            // Find a club that is run by a chap called 'Daryl Loynes'
            var matchingClub = clubTasks.GetByContactName("Daryl Loynes");

            Console.WriteLine(matchingClub);

            // Update the Club's Phone Number
            matchingClub.Contact.Tel = "555-123-456";

            // Push the updated object back to MongoDB
            clubTasks.Update(matchingClub);

            // Ensure the document has been updated
            var updatedClub = clubTasks.GetByContactName("Daryl Loynes");

            Console.WriteLine(updatedClub);
        }
    }
}