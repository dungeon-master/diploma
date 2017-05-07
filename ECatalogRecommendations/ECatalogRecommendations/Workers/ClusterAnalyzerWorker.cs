using System;
using System.Collections.Generic;
using System.ComponentModel;
using ECatalogRecommendations.Analyzers;
using ECatalogRecommendations.Enums;
using ECatalogRecommendations.Models;

namespace ECatalogRecommendations.Workers
{
    public class ClusterAnalyzerWorker
    {
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private readonly ClusterAnalyzer _clusterAnalyzer = new ClusterAnalyzer();

        private readonly InitProgressBarCallback _initProgressBar;
        private readonly UpdateProgressBarCallback _updateProgressBar;
        private List<Tuple<SearchRequest, double>> _requests;
        private List<ExternalCatalogBook> _books;

        public ClusterAnalyzerWorker(InitProgressBarCallback initProgressBar, UpdateProgressBarCallback updateProgressBar)
        {
            _initProgressBar = initProgressBar;
            _updateProgressBar = updateProgressBar;

            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += OnDoWork;
            _worker.ProgressChanged += OnProgressChanged;
            _worker.RunWorkerCompleted += OnWorkerCompleted;
        }

        public void RunWorker(List<Tuple<SearchRequest, double>> requests, List<ExternalCatalogBook> books)
        {
            if (!_worker.IsBusy)
            {
                _requests = requests;
                _books = books;
                _initProgressBar(requests.Count);
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
                return _clusterAnalyzer.GetResult();
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
          
            _clusterAnalyzer.Analyze(_requests, _books, ref worker, ref e);
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
                _updateProgressBar(0, BackgroundWorkerState.ClusterAnalyzerDone);
            }
        }
    }
}
