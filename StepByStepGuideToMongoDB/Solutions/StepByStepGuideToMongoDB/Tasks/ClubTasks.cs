namespace StepByStepGuideToMongoDB.Tasks
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;

    using StepByStepGuideToMongoDB.Contracts.Framework.Infrastructure.Repositories;
    using StepByStepGuideToMongoDB.Contracts.Infrastructure.Repositories;
    using StepByStepGuideToMongoDB.Contracts.Tasks;
    using StepByStepGuideToMongoDB.Domain;
    using StepByStepGuideToMongoDB.Infrastructure.Specifications;

    #endregion

    public class ClubTasks : IClubTasks
    {
        private readonly IRemoteClubDataSource remoteClubDataSource;
        private readonly IWritableLinqRepository<Club> clubRepository;

        public ClubTasks(IRemoteClubDataSource remoteClubDataSource, IWritableLinqRepository<Club> clubRepository)
        {
            this.remoteClubDataSource = remoteClubDataSource;
            this.clubRepository = clubRepository;
        }

        public IEnumerable<Club> RetrieveClubsFromRemoteDataSource()
        {
            return this.remoteClubDataSource.GetAllClubs();
        }

        public void AddToLocalRepository(IEnumerable<Club> clubs)
        {
            foreach (var club in clubs)
            {
                this.clubRepository.Save(club);
            }
        }

        public Club GetByContactName(string contactName)
        {
            return this.clubRepository.FindAll(new ByContactNameSpecification(contactName)).FirstOrDefault();
        }

        public void Update(Club club)
        {
            this.clubRepository.Update(club);
        }
    }
}