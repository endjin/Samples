namespace StepByStepGuideToMongoDB.Infrastructure.Specifications
{
    #region Using Directives

    using StepByStepGuideToMongoDB.Domain;
    using StepByStepGuideToMongoDB.Framework.Specifications;

    #endregion

    public class ByContactNameSpecification : QuerySpecification<Club>
    {
        private readonly string name;

        public ByContactNameSpecification(string name)
        {
            this.name = name;
        }

        public override System.Linq.Expressions.Expression<System.Func<Club, bool>> MatchingCriteria
        {
            get { return c => c.Contact.Name == this.name; }
        }
    }
}