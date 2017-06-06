using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using ECatalogRecommendations.Models;
using ECatalogRecommendations.Extensions;

namespace ECatalogRecommendations.Settings
{
    [Serializable]
    public class GeneralSettings
    {
        [XmlArray("DeletedRecommendations"), XmlArrayItem("DeletedRecommendation")]
        public List<CustomKeyValuePair<ExternalCatalogBook, double>> DeletedRecommendations;
        [XmlArray("Recommendations"), XmlArrayItem("Recommendation")]
        public List<CustomKeyValuePair<ExternalCatalogBook, double>> Recommendations;

        public bool UseMaxForAnalyze;
        public decimal MaxValueForAnalyze;
        public decimal ExternalCatalogType;
        public string ExternalCatalogPath;

        public GeneralSettings() { }
    }
}
