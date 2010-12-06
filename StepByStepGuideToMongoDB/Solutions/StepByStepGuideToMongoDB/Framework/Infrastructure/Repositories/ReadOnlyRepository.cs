namespace StepByStepGuideToMongoDB.Framework.Infrastructure.Repositories
{
    #region Using Directives

    using System.Linq;

    using StepByStepGuideToMongoDB.Contracts.Framework.Infrastructure.Repositories;
    using StepByStepGuideToMongoDB.Contracts.Framework.Infrastructure.Specifications;
    using StepByStepGuideToMongoDB.Framework.Infrastructure.Norm;

    #endregion

    public class ReadOnlyRepository<T> : IReadOnlyLinqRepository<T>
    {
        public T FindOne(ILinqSpecification<T> specification)
        {
            return this.FindAll(specification).FirstOrDefault();
        }

        public IQueryable<T> FindAll()
        {
            using (var session = new Session<T>(typeof(T).Name))
            {
                return session.Queryable;
            }
        }

        public IQueryable<T> FindAll(ILinqSpecification<T> specification)
        {
            using (var session = new Session<T>(typeof(T).Name))
            {
                return specification.SatisfyingElementsFrom(session.Queryable);
            }
        }
    }
}