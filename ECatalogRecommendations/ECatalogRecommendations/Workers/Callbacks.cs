using ECatalogRecommendations.Enums;

namespace ECatalogRecommendations.Workers
{
    public delegate void InitProgressBarCallback(int size);
    public delegate void UpdateProgressBarCallback(int progress, BackgroundWorkerState state);
}
