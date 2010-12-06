namespace StepByStepGuideToMongoDB.Domain
{
    #region Using Directives

    using System.Text;

    using StepByStepGuideToMongoDB.Framework.Extensions;

    #endregion

    public class Contact
    {
        public string Address { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Tel { get; set; }

        public string Web { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendIfValueNotEmpty("\tName: ", this.Name);
            sb.AppendIfValueNotEmpty("\tPosition: ", this.Position);
            sb.AppendIfValueNotEmpty("\tAddress: ", this.Address);
            sb.AppendIfValueNotEmpty("\tTel: ", this.Tel);
            sb.AppendIfValueNotEmpty("\tWeb: ", this.Web);
            sb.AppendIfValueNotEmpty("\tEmail: ", this.Email);

            return sb.ToString();
        }
    }
}