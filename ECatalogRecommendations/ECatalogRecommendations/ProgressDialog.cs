using System;
using System.Windows.Forms;
using ECatalogRecommendations.Enums;
using ECatalogRecommendations.Workers;
using ECatalogRecommendations.Models;

namespace ECatalogRecommendations
{
    public partial class ProgressDialog : Form
    {
        private readonly ActionAnalyzerWorker _actionAnalyzerWorker;
        private readonly ExternalCatalogReaderWorker _externalCatalogReaderWorker;
        private readonly ClusterAnalyzerWorker _clusterAnalyzerWorker;
        private readonly CatalogCheckerWorker _catalogCheckerWorker;

        private Settings.GeneralSettings _settings;
        private readonly MainForm _parent;

        public ProgressDialog(MainForm parent)
        {
            InitializeComponent();

            _parent = parent;

            _actionAnalyzerWorker = new ActionAnalyzerWorker(UpdateProgressBar);
            _externalCatalogReaderWorker = new ExternalCatalogReaderWorker(UpdateProgressBar);
            _clusterAnalyzerWorker = new ClusterAnalyzerWorker(InitProgressBar, UpdateProgressBar);
            _catalogCheckerWorker = new CatalogCheckerWorker(InitProgressBar, UpdateProgressBar);
        }

        public void SetSettings(Settings.GeneralSettings settings)
        {
            _settings = settings;
        }

        private void Start()
        {
            lblStatus.Text = "Инициализация...";
            progressBar.Style = ProgressBarStyle.Marquee;
            _actionAnalyzerWorker.RunWorker();
        }

        private void InitProgressBar(int maximum)
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = maximum;
            progressBar.Value = 0;
        }

        public void SetAnalyzeMaximum(int maximum)
        {
            Proxy.CatalogProxy.Maximum = maximum;
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
                    lblStatus.Text = string.Format("Шаг 1 из 3, анализ действий пользователей. \nПроанализировано действий {0} из {1}", progress.ToString(), progressBar.Maximum);
                    progressBar.Style = ProgressBarStyle.Continuous;
                    progressBar.Value = progress;
                    break;
                }
                case BackgroundWorkerState.ExternalCatalogReportProgress:
                {
                    lblStatus.Text = string.Format("Шаг 2 из 3, считывание данных из внешнего каталога. \nСчитано документов {0} из {1}", progress.ToString(), progressBar.Maximum);
                    progressBar.Style = ProgressBarStyle.Continuous;
                    progressBar.Value = progress;
                    break;
                }
                case BackgroundWorkerState.ClusterAnalyzerReportProgress:
                {
                    lblStatus.Text = string.Format("Шаг 3 из 3, кластеризация запросов. \nОбработано запросов {0} из {1}", progress.ToString(), progressBar.Maximum);
                    progressBar.Style = ProgressBarStyle.Continuous;
                    progressBar.Value = progress;
                    break;
                }
                case BackgroundWorkerState.CatalogCheckerReportProgress:
                {
                    lblStatus.Text = progress.ToString();
                    progressBar.Style = ProgressBarStyle.Continuous;
                    progressBar.Value = progress;
                    break;
                }
                case BackgroundWorkerState.ActionAnalyzerDone:
                {
                    progressBar.Value = progressBar.Maximum;
                    lblStatus.Text = "Шаг 1 завершен. \nЗапросов для дальнейшего анализа: " + _actionAnalyzerWorker.GetResult().Count;
                    string path = _settings.ExternalCatalogType == Constants.USE_XML ? _settings.ExternalCatalogPath : null;
                    _externalCatalogReaderWorker.RunWorker(path);
                    break;
                }
                case BackgroundWorkerState.ExternalCatalogDone:
                {
                    progressBar.Value = progressBar.Maximum;
                    lblStatus.Text = "Шаг 2 завершен. \nСчитано документов: " + _externalCatalogReaderWorker.GetResult().Count;
                    _clusterAnalyzerWorker.RunWorker(_actionAnalyzerWorker.GetResult(), _externalCatalogReaderWorker.GetResult());
                    break;
                }
                case BackgroundWorkerState.ClusterAnalyzerDone:
                {
                    progressBar.Value = progressBar.Maximum;
                    lblStatus.Text = "Шаг 3 завершен. \nПолучено рекомендаций: " + _clusterAnalyzerWorker.GetResult().Count;
                    _parent.ShowResult(_clusterAnalyzerWorker.GetResult());
                    Close();
                    break;
                }
                case BackgroundWorkerState.CatalogCheckerDone:
                {
                    progressBar.Value = progressBar.Maximum;
                    lblStatus.Text = "CatalogCheckerDone! Total = " + _catalogCheckerWorker.GetResult().Count;
                    break;
                }
            }
        }

        private void Stop()
        {
            _actionAnalyzerWorker.StopWorker();
            _externalCatalogReaderWorker.StopWorker();
            _clusterAnalyzerWorker.StopWorker();
            InitProgressBar(progressBar.Maximum);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ProgressDialog_Shown(object sender, EventArgs e)
        {
            Start();
        }

        private void ProgressDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }
    }
}
