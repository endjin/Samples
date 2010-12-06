namespace StepByStepGuideToMongoDB.Contracts.Framework.Infrastructure.Repositories
{
    public interface IWritableLinqRepository<T> : IReadOnlyLinqRepository<T>
    {
        void Delete(T item);

        void Save(T item);

        void Update(T item);
    }
}