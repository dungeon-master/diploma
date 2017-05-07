namespace ECatalogRecommendations.Enums
{
    public enum BackgroundWorkerState
    {
        ActionAnalyzerInitialize,
        ActionAnalyzerReportProgress,
        ActionAnalyzerDone,
        ExternalCatalogInitialize,
        ExternalCatalogReportProgress,
        ExternalCatalogDone,
        ClusterAnalyzerReportProgress,
        ClusterAnalyzerDone,
        CatalogCheckerReportProgress,
        CatalogCheckerDone
    }
}
