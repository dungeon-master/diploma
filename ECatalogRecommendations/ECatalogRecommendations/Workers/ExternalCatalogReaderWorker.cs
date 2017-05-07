using System.Collections.Generic;
using System.ComponentModel;
using ECatalogRecommendations.Enums;
using ECatalogRecommendations.Models;
using ECatalogRecommendations.Proxy;

namespace ECatalogRecommendations.Workers
{
    public class ExternalCatalogReaderWorker
    {
        private const int ReportValue = 1000;

        private readonly BackgroundWorker _worker = new BackgroundWorker();

        private readonly UpdateProgressBarCallback _updateProgressBar;

        private string _xmlPath;
        private List<ExternalCatalogBook> _result = null;

        public ExternalCatalogReaderWorker(UpdateProgressBarCallback updateProgressBar)
        {
            _updateProgressBar = updateProgressBar;

            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += OnDoWork;
            _worker.ProgressChanged += OnProgressChanged;
            _worker.RunWorkerCompleted += OnWorkerCompleted;
        }

        public void RunWorker(string xmlPath)
        {
            if (!_worker.IsBusy)
            {
                _xmlPath = xmlPath;
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

        public List<ExternalCatalogBook> GetResult()
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

            if (_xmlPath != null)
            {
                _result = ExternalCatalogProxy.GetBooks(_xmlPath, ReportValue / 10, ref worker, ref e);
            }
            else
            {
                _result = ExternalCatalogProxy.GetBooks(ReportValue, ref worker, ref e);
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
                _updateProgressBar(0, BackgroundWorkerState.ExternalCatalogDone);
            }
        }
    }
}
