using System;
using System.Collections.Generic;
using System.ComponentModel;
using ECatalogRecommendations.Analyzers;
using ECatalogRecommendations.Enums;
using ECatalogRecommendations.Models;
using ECatalogRecommendations.Proxy;

namespace ECatalogRecommendations.Workers
{
    public class ActionAnalyzerWorker
    {
        private const int ReportValue = 1000;

        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private IActionAnalyzer _actionAnalyzer;

        private readonly UpdateProgressBarCallback _updateProgressBar;

        private bool _runWhenReady;

        public ActionAnalyzerWorker(UpdateProgressBarCallback updateProgressBar)
        {
            _updateProgressBar = updateProgressBar;

            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += OnDoWork;
            _worker.ProgressChanged += OnProgressChanged;
            _worker.RunWorkerCompleted += OnWorkerCompleted;

            _runWhenReady = false;
        }

        public void RunWorker()
        {
            if (!_worker.IsBusy)
            {
                _actionAnalyzer = new ActionAnalyzer();
                _worker.RunWorkerAsync();
            }
            else
            {
                _runWhenReady = true;
            }
        }

        public void StopWorker()
        {
            if (_worker.WorkerSupportsCancellation && _worker.IsBusy)
            {
                _worker.CancelAsync();
            }
        }

        public List<Tuple<SearchRequest, double>> GetResult()
        {
            if (!_worker.IsBusy)
            {
                return _actionAnalyzer.GetResult();
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

            _runWhenReady = false;

            int size = CatalogProxy.GetLogSize();
            worker.ReportProgress(size, BackgroundWorkerState.ActionAnalyzerInitialize);

            CatalogProxy.QueryLog(ref _actionAnalyzer, ReportValue, ref worker, ref e);
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
                _updateProgressBar(0, BackgroundWorkerState.ActionAnalyzerDone);
            }
            else if (_runWhenReady)
            {
                RunWorker();
            }
        }
    }
}
