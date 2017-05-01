using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECatalogRecommendations
{
    public  static class SearchFields
    {
        public static string Responsible = "Responsible";
        public static string GrntiText = "GrntiText";
        public static string Keywords = "Keywords";
        public static string Title = "Title";
        public static string DigitalResources = "DigitalResources";
        public static string UdcIndex = "UdcIndex";
        public static string ReceiveDateLower = "ReceiveDateLower";
        public static string Header = "Header";
        public static string SubjectId = "SubjectId";
        public static string PublishYearLower = "PublishYearLower";
        public static string PublishYearUpper = "PublishYearUpper";
        public static string Publisher = "Publisher";
        public static string SubjectText = "SubjectText";
        public static string BbkIndex = "BbkIndex";
        public static string ISBN = "ISBN";
        public static string ISSN = "ISSN";
        public static string InventoryNumber = "InventoryNumber";
        public static string AvailableForOrder = "AvailableForOrder";
        public static string DocumentType = "DocumentType";
        public static string VinitiPublicationDateLower = "VinitiPublicationDateLower";
        public static string VinitiPublicationDateUpper = "VinitiPublicationDateUpper";
        public static string VinitiSubTopicGuid = "VinitiSubTopicGuid";
        public static string UdcText = "UdcText";
        public static string VinitiLanguage = "VinitiLanguage";
        public static string FundIds = "FundIds";
        public static string VinitiCountry = "VinitiCountry";
        public static string VinitiDocType = "VinitiDocType";
        public static string GrntiIndex = "GrntiIndex";

        public static string[] GetSearchFields()
        {
            return new[] { Keywords, Responsible, Title, SubjectText };
        }
    }
}
