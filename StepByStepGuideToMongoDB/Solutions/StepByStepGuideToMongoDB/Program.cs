namespace StepByStepGuideToMongoDB
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition.Hosting;
    using System.Web;

    using Endjin.Core.Container;
    using Endjin.Core.Windsor.Container;

    using StepByStepGuideToMongoDB.Contracts.Tasks;
    using StepByStepGuideToMongoDB.Domain;
    using StepByStepGuideToMongoDB.Infrastructure.DataSources;
    using StepByStepGuideToMongoDB.Tasks;

    #endregion

    public static class Program
    {
        private static void Main(string[] args)
        {
            Initialize();
            DoWork();
        }

        private static void Initialize()
        {
            ApplicationServiceLocator.Initialize(
                new WindsorServiceContainer(), 
                new MefWindsorBootstrapper(new AssemblyCatalog(typeof(Program).Assembly)));
        }

        private static void DoWork()
        {
            var clubTasks = ApplicationServiceLocator.Container.Resolve<IClubTasks>();

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