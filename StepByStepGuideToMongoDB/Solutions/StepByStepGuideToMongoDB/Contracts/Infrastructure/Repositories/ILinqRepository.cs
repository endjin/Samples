namespace StepByStepGuideToMongoDB.Contracts.Infrastructure.Repositories
{
    #region Using Directives

    using System.Linq;

    using StepByStepGuideToMongoDB.Contracts.Framework.Infrastructure.Specifications;

    #endregion

    public interface ILinqRepository<T>
    {
        void Delete(T item);

        /// <summary>
        /// Finds all items within the repository.
        /// </summary>
        /// <typeparam name="T">Type of entity to find</typeparam>
        /// <returns>All items in the repository</returns>
        IQueryable<T> FindAll();

        /// <summary>
        /// Finds all items by a specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <typeparam name="T">Type of entity to find</typeparam>
        /// <returns>All matching items</returns>
        IQueryable<T> FindAll(ILinqSpecification<T> specification);

        /// <summary>
        /// Finds an item by a specification
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <typeparam name="T">Type of entity to find</typeparam>
        /// <returns>The the matching item</returns>
        T FindOne(ILinqSpecification<T> specification);

        void Save(T item);

        void Update(T item);
    }
}