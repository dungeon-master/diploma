using System.Collections.Generic;
using System.ComponentModel;
using ECatalogRecommendations.Enums;
using ECatalogRecommendations.Models;
using ECatalogRecommendations.Proxy;

namespace ECatalogRecommendations.Workers
{
    public class CatalogCheckerWorker
    {
        private const int ReportValue = 100;

        private readonly BackgroundWorker _worker = new BackgroundWorker();

        private readonly InitProgressBarCallback _initProgressBar;
        private readonly UpdateProgressBarCallback _updateProgressBar;
        private Dictionary<ExternalCatalogBook, double> _clusterResult;
        private Dictionary<ExternalCatalogBook, double> _result = null;

        public CatalogCheckerWorker(InitProgressBarCallback initProgressBar, UpdateProgressBarCallback updateProgressBar)
        {
            _initProgressBar = initProgressBar;
            _updateProgressBar = updateProgressBar;

            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += OnDoWork;
            _worker.ProgressChanged += OnProgressChanged;
            _worker.RunWorkerCompleted += OnWorkerCompleted;
        }

        public void RunWorker(Dictionary<ExternalCatalogBook, double> clusterResult)
        {
            if (!_worker.IsBusy)
            {
                _clusterResult = clusterResult;
                _initProgressBar(clusterResult.Count);
                _worker.RunWorkerAsync();
            }
        }

        public void StopWorker()
        {
            if (_worker.WorkerSupportsCancellation && _worker.IsBusy)
            {
                _worker.CancelAsync();
            }
        }

        public Dictionary<ExternalCatalogBook, double> GetResult()
        {
            if (!_worker.IsBusy)
            {
                return _result;
            }
            return null;
        }

        private void OnDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker == null)
            {
                return;
            }

            _result = new Dictionary<ExternalCatalogBook, double>();

            int count = 0;
            foreach (var r in _clusterResult)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                if (!CatalogProxy.IsPresentInCatalog(r.Key.Title))
                {
                    _result.Add(r.Key, r.Value);
                }
                count++;
                if (count % ReportValue == 0)
                {
                    worker.ReportProgress(count, BackgroundWorkerState.CatalogCheckerReportProgress);
                }
            }
        }

        private void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorkerState state = (BackgroundWorkerState)e.UserState;
            _updateProgressBar(e.ProgressPercentage, state);
        }

        private void OnWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                _updateProgressBar(0, BackgroundWorkerState.CatalogCheckerDone);
            }
        }
    }
}
