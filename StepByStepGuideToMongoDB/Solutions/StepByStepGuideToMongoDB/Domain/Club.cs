namespace StepByStepGuideToMongoDB.Domain
{
    #region Using Directives

    using System;
    using System.Text;

    using StepByStepGuideToMongoDB.Contracts.Framework.Infrastructure.Repositories;
    using StepByStepGuideToMongoDB.Framework.Extensions;

    #endregion

    public class Club : IUniqueIdentifier
    {
        public Club()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Ages { get; set; }

        public Contact Contact { get; set; }

        public string Facility { get; set; }

        public string Id { get; set; }

        public string Info { get; set; }

        public string Leagues { get; set; }

        public string Name { get; set; }

        public string Sport { get; set; }

        public string Training { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendIfValueNotEmpty("Name: ", this.Name);
            sb.AppendIfValueNotEmpty("Facility: ", this.Facility);
            sb.AppendIfValueNotEmpty("Info: ", this.Info);
            sb.AppendIfValueNotEmpty("Leagues: ", this.Leagues);
            sb.AppendIfValueNotEmpty("Sport: ", this.Sport);
            sb.AppendIfValueNotEmpty("Training: ", this.Training);
            sb.AppendLine("Contact: ");
            sb.AppendLine(this.Contact.ToString());
            
            return sb.ToString();
        }
    }
}