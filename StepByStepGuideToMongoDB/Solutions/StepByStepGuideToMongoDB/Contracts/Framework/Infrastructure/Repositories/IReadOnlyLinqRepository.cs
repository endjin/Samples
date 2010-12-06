namespace StepByStepGuideToMongoDB.Contracts.Framework.Infrastructure.Repositories
{
    #region Using Directives

    using System.Linq;

    using StepByStepGuideToMongoDB.Contracts.Framework.Infrastructure.Specifications;

    #endregion

    /// <summary>
    /// Defines a LINQ implementation of the Repository Pattern that takes in a Specification to define
    /// the items that should be returned.
    /// </summary>
    /// <typeparam name="T">
    /// Type to be retrieved from readonly store
    /// </typeparam>
    public interface IReadOnlyLinqRepository<T>
    {
        /// <summary>
        /// Finds an item by a specification
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <typeparam name="T">Type of entity to find</typeparam>
        /// <returns>The the matching item</returns>
        T FindOne(ILinqSpecification<T> specification);

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
    }
}