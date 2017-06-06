using System;
using System.Collections.Generic;
using ECatalogRecommendations.DbEntities;
using ECatalogRecommendations.Models;

namespace ECatalogRecommendations.Analyzers
{
    public interface IActionAnalyzer
    {
        void AnalyzeAction(FrontOfficeAction action);
        List<Tuple<SearchRequest, double>> GetResult();
    }
}
