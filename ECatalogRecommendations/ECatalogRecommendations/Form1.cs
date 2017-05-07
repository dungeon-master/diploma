using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECatalogRecommendations.Analyzers;
using ECatalogRecommendations.Enums;
using ECatalogRecommendations.Models;
using ECatalogRecommendations.Proxy;
using ECatalogRecommendations.Workers;

namespace ECatalogRecommendations
{
    public partial class MainForm : Form
    {
        private readonly ActionAnalyzerWorker _actionAnalyzerWorker;
        private readonly ExternalCatalogReaderWorker _externalCatalogReaderWorker;
        private readonly ClusterAnalyzerWorker _clusterAnalyzerWorker;
        private readonly CatalogCheckerWorker _catalogCheckerWorker;

        public MainForm()
        {
            InitializeComponent();

            _actionAnalyzerWorker = new ActionAnalyzerWorker(UpdateProgressBar);
            _externalCatalogReaderWorker = new ExternalCatalogReaderWorker(UpdateProgressBar);
            _clusterAnalyzerWorker = new ClusterAnalyzerWorker(InitProgressBar, UpdateProgressBar);
            _catalogCheckerWorker = new CatalogCheckerWorker(InitProgressBar, UpdateProgressBar);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            label1.Text = "Initializing";
            progressBar.Style = ProgressBarStyle.Marquee;
            _actionAnalyzerWorker.RunWorker();
        }

        private void InitProgressBar(int maximum)
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = maximum;
            progressBar.Value = 0;
        }

        private void UpdateProgressBar(int progress, BackgroundWorkerState state)
        {
            switch (state)
            {
                case BackgroundWorkerState.ActionAnalyzerInitialize:
                {
                    InitProgressBar(progress);
                    break;
                }
                case BackgroundWorkerState.ExternalCatalogInitialize:
                {
                    InitProgressBar(progress);
                    break;
                }
                case BackgroundWorkerState.ActionAnalyzerReportProgress:
                {
                    label1.Text = progress.ToString();
                    progressBar.Style = ProgressBarStyle.Continuous;
                    progressBar.Value = progress;
                    break;
                }
                case BackgroundWorkerState.ExternalCatalogReportProgress:
                {
                    label1.Text = progress.ToString();
                    progressBar.Style = ProgressBarStyle.Continuous;
                    progressBar.Value = progress;
                    break;
                }
                case BackgroundWorkerState.ClusterAnalyzerReportProgress:
                {
                    label1.Text = progress.ToString();
                    progressBar.Style = ProgressBarStyle.Continuous;
                    progressBar.Value = progress;
                    break;
                }
                case BackgroundWorkerState.CatalogCheckerReportProgress:
                {
                    label1.Text = progress.ToString();
                    progressBar.Style = ProgressBarStyle.Continuous;
                    progressBar.Value = progress;
                    break;
                }
                case BackgroundWorkerState.ActionAnalyzerDone:
                {
                    progressBar.Value = progressBar.Maximum;
                    label1.Text = "ActionAnalyzerDone! Total = " + _actionAnalyzerWorker.GetResult().Count;
                    _externalCatalogReaderWorker.RunWorker(null);
                    break;
                }
                case BackgroundWorkerState.ExternalCatalogDone:
                {
                    progressBar.Value = progressBar.Maximum;
                    label1.Text = "ExternalCatalogDone! Total = " + _externalCatalogReaderWorker.GetResult().Count;
                    _clusterAnalyzerWorker.RunWorker(_actionAnalyzerWorker.GetResult(), _externalCatalogReaderWorker.GetResult());
                    break;
                }
                case BackgroundWorkerState.ClusterAnalyzerDone:
                {
                    progressBar.Value = progressBar.Maximum;
                    label1.Text = "ClusterAnalyzerDone! Total = " + _clusterAnalyzerWorker.GetResult().Count;
                    _catalogCheckerWorker.RunWorker(_clusterAnalyzerWorker.GetResult());
                    break;
                }
                case BackgroundWorkerState.CatalogCheckerDone:
                {
                    progressBar.Value = progressBar.Maximum;
                    label1.Text = "CatalogCheckerDone! Total = " + _catalogCheckerWorker.GetResult().Count;
                    break;
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _actionAnalyzerWorker.StopWorker();
            _externalCatalogReaderWorker.StopWorker();
            _clusterAnalyzerWorker.StopWorker();
            InitProgressBar(progressBar.Maximum);
            label1.Text = "Stopped!";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            



            //ExternalCatalogProxy.GetBooks();






            // WRITER TO FILE
            /*
            using (StreamWriter writer = new StreamWriter("books.txt"))
            {
                foreach (var element in _actionAnalyzer.GetResult().OrderByDescending(o => o.Value))
                {
                    writer.WriteLine(element.Key.ToString() + " Weight: " + element.Value + " Session: " + element.Key.Session);
                }
            }
            label1.Text = "Writer finished";
            */





            // GOOGLE
            //string query = "ГОСТ гайки накидные";
            //var customSearchService = new CustomsearchService(new BaseClientService.Initializer {ApiKey = GoogleApiKey});
            //var listRequest = customSearchService.Cse.List(query);
            //listRequest.Cx = SearchEngineId;
            //var result = listRequest.Execute().Items;
            //foreach (var item in result)
            //{
            //    label1.Text = item.Title;
            //}
        }
    }
}
