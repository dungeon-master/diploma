using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ECatalogRecommendations
{
    public partial class Form1 : Form
    {
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private readonly ActionAnalyzer _actionAnalyzer = new ActionAnalyzer();

        public Form1()
        {
            InitializeComponent();

            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += new DoWorkEventHandler(bw_DoWork);
            _worker.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!_worker.IsBusy)
            {
                int size;
                using (var db = new LibraryLogModel())
                {
                    size = db.FrontOfficeAction.Count();
                }
                progressBar.Minimum = 0;
                progressBar.Maximum = size;
                _worker.RunWorkerAsync();
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker == null)
            {
                return;
            }

            worker.ReportProgress(-1);
            using (var db = new LibraryLogModel())
            {
                int count = 0;
                var table = db.FrontOfficeAction.AsNoTracking()
                    .OrderBy(s => s.FrontOfficeSessionId)
                    .ThenBy(d => d.ActionDateTime);
                foreach (var row in table)
                {
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;                                          
                        return;
                    }
                    count++;
                    _actionAnalyzer.AnalyzeAction(row);
                    if (count % 1000 == 0)
                    {
                        worker.ReportProgress(count);
                    }
                }
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            if (progress == -1)
            {
                label1.Text = "Initializing";
                progressBar.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                label1.Text = e.ProgressPercentage.ToString();
                progressBar.Style = ProgressBarStyle.Continuous;
                progressBar.Value = e.ProgressPercentage;
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                label1.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                label1.Text = "Error: " + e.Error.Message;
            }
            else
            {
                label1.Text = "Done! Total = " + _actionAnalyzer.GetResult().Count;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_worker.WorkerSupportsCancellation && _worker.IsBusy)
            {
                _worker.CancelAsync();
            }
        }
    }
}
