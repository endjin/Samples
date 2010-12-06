namespace StepByStepGuideToMongoDB.Infrastructure.DataSources
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Xml.Linq;

    using StepByStepGuideToMongoDB.Contracts.Infrastructure.Repositories;
    using StepByStepGuideToMongoDB.Domain;

    #endregion

    public class RemoteClubDataSource : IRemoteClubDataSource
    {
        public IEnumerable<Club> GetAllClubs()
        {
            // List of clubs released from http://data.gov.uk
            var xmlSource = XElement.Load("http://www.lichfielddc.gov.uk/site/custom_scripts/club_xml.php");

            // Converts to a list of POCO objects
            return from item in xmlSource.Descendants("club")
                   select new Club
                   {
                       Ages = item.Element("ages").Value,
                       Facility = item.Element("facility").Value,
                       Info = HttpUtility.HtmlDecode(item.Element("info").Value),
                       Leagues = item.Element("leagues").Value,
                       Name = item.Element("name").Value,
                       Sport = item.Element("sport").Value,
                       Training = item.Element("training").Value.Replace("<br />", string.Empty),
                       Contact = new Contact
                       {
                           Address = item.Element("contact").Element("address").Value.Replace("<br />", string.Empty),
                           Email = item.Element("contact").Element("email").Value,
                           Name = item.Element("contact").Element("name").Value,
                           Position = item.Element("contact").Element("position").Value,
                           Tel = item.Element("contact").Element("tel").Value,
                           Web = item.Element("contact").Element("web").Value
                       }
                   };
        }
    }
}