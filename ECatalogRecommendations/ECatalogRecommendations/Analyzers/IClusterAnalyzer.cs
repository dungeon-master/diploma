using System;
using System.Collections.Generic;
using ECatalogRecommendations.Models;
using System.ComponentModel;

namespace ECatalogRecommendations.Analyzers
{
    public interface IClusterAnalyzer
    {
        void Analyze(List<Tuple<SearchRequest, double>> requests, List<ExternalCatalogBook> books,
            ref BackgroundWorker worker, ref DoWorkEventArgs e);
        List<KeyValuePair<ExternalCatalogBook, double>> GetResult();
    }
}
