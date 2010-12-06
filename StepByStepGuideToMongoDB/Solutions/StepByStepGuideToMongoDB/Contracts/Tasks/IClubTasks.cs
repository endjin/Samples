namespace StepByStepGuideToMongoDB.Contracts.Tasks
{
    #region Using Directives

    using System.Collections.Generic;

    using StepByStepGuideToMongoDB.Domain;

    #endregion

    public interface IClubTasks
    {
        IEnumerable<Club> RetrieveClubsFromRemoteDataSource();

        void AddToLocalRepository(IEnumerable<Club> clubs);

        Club GetByContactName(string contactName); 

        void Update(Club club);
    }
}