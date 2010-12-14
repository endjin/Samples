namespace StepByStepGuideToMongoDB.Infrastructure.Norm
{
    #region Using Directives

    using System;
    using System.Linq;

    using global::Norm;

    using StepByStepGuideToMongoDB.Contracts.Infrastructure.Repositories;

    #endregion

    internal class Session<TEntity> : IDisposable
    {
        private readonly IMongo provider;

        public Session(string database)
        {
            this.provider = Mongo.Create(database);
        }

        public IQueryable<TEntity> Queryable
        {
            get { return this.Provider.GetCollection<TEntity>().AsQueryable(); }
        }

        public IMongo Provider
        {
            get { return this.provider; }
        }

        public void Add<T>(T item) where T : class, new()
        {
            this.provider.Database.GetCollection<T>().Insert(item);
        }

        public void Dispose()
        {
            this.provider.Dispose();
        }

        public void Drop<T>()
        {
            this.provider.Database.DropCollection(typeof(T).Name);
        }

        public void Delete<T>(T item) where T : class, new()
        {
            this.provider.GetCollection<T>().Delete(item);
        }

        public void Update<T>(T item) where T : class, IUniqueIdentifier, new()
        {
            this.provider.Database.GetCollection<T>().UpdateOne(new { id = item.Id }, item);
        }
    }
}