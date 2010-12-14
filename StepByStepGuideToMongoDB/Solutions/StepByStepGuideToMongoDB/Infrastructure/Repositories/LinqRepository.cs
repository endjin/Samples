namespace StepByStepGuideToMongoDB.Infrastructure.Repositories
{
    #region Using Directives

    using System.Linq;

    using StepByStepGuideToMongoDB.Contracts.Framework.Infrastructure.Specifications;
    using StepByStepGuideToMongoDB.Contracts.Infrastructure.Repositories;
    using StepByStepGuideToMongoDB.Infrastructure.Norm;

    #endregion

    public class LinqRepository<T> : ILinqRepository<T> where T : class, IUniqueIdentifier, new()
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