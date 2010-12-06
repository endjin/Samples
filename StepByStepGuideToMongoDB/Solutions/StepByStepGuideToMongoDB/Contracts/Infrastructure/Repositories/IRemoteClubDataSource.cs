namespace StepByStepGuideToMongoDB.Contracts.Infrastructure.Repositories
{
    #region Using Directives

    using System.Collections.Generic;

    using StepByStepGuideToMongoDB.Domain;

    #endregion

    public interface IRemoteClubDataSource
    {
        IEnumerable<Club> GetAllClubs();
    }
}