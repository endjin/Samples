namespace StepByStepGuideToMongoDB.Framework.Infrastructure.Repositories
{
    #region Using Directives

    using StepByStepGuideToMongoDB.Contracts.Framework.Infrastructure.Repositories;
    using StepByStepGuideToMongoDB.Framework.Infrastructure.Norm;

    #endregion

    public class ReadWriteRepository<T> : ReadOnlyRepository<T>, IWritableLinqRepository<T> where T : class, IUniqueIdentifier, new()
    {
        public void Delete(T item)
        {
            using (var session = new Session<T>(typeof(T).Name))
            {
                session.Delete(item);
            }
        }

        public void Save(T item)
        {
            using (var session = new Session<T>(typeof(T).Name))
            {
                session.Add(item);
            }
        }

        public void Update(T item)
        {
            using (var session = new Session<T>(typeof(T).Name))
            {
                session.Update(item);
            }
        }
    }
}