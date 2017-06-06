using System;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Windows.Forms;
using ECatalogRecommendations.Models;
using ECatalogRecommendations.Extensions;

namespace ECatalogRecommendations
{
    public partial class MainForm : Form
    {
        private const string SETTINGS_PATH = "settings.xml";
        private readonly ProgressDialog _progressDialog;

        private Settings.GeneralSettings _generalSettings;
        private bool _hasDeleted;

        public MainForm()
        {
            InitializeComponent();

            _progressDialog = new ProgressDialog(this);

            if (File.Exists(SETTINGS_PATH))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Settings.GeneralSettings));

                using (FileStream fs = new FileStream(SETTINGS_PATH, FileMode.Open))
                {
                    _generalSettings = (Settings.GeneralSettings) formatter.Deserialize(fs);
                    if (_generalSettings.Recommendations.Count > 0)
                    {
                        ShowResult(_generalSettings.Recommendations);
                    }
                }
            }
            else
            {
                _generalSettings = new Settings.GeneralSettings();
                _generalSettings.DeletedRecommendations = new List<CustomKeyValuePair<ExternalCatalogBook, double>>();
                _generalSettings.Recommendations = new List<CustomKeyValuePair<ExternalCatalogBook, double>>();
                _generalSettings.UseMaxForAnalyze = false;
                _generalSettings.MaxValueForAnalyze = 100;
                _generalSettings.ExternalCatalogType = Constants.USE_DATABASE;
                _generalSettings.ExternalCatalogPath = null;
            }
        }

        private void openProgressDialog()
        {
            _progressDialog.SetSettings(_generalSettings);
            _progressDialog.ShowDialog();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            openProgressDialog();
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openProgressDialog();
        }

        public void ShowResult(List<KeyValuePair<ExternalCatalogBook, double>> result)
        {
            if (result.Count > 0)
            {
                List<CustomKeyValuePair<ExternalCatalogBook, double>> list = new List<CustomKeyValuePair<ExternalCatalogBook, double>>();
                foreach (KeyValuePair<ExternalCatalogBook, double> book in result)
                {
                    list.Add(new CustomKeyValuePair<ExternalCatalogBook, double>(book.Key, book.Value));
                }
                _generalSettings.Recommendations.Clear();
                ShowResult(list);
            }
            else
            {
                MessageBox.Show("Не удалось сформировать рекомендации по имеющимся данным");
            }
        }

        public void ShowResult(List<CustomKeyValuePair<ExternalCatalogBook, double>> result)
        {
            gridResult.Rows.Clear();
            gridResult.Refresh();
            if (_generalSettings.Recommendations.Count == 0)
            {
                _generalSettings.Recommendations.AddRange(result);
            }           
            foreach (CustomKeyValuePair<ExternalCatalogBook, double> book in result)
            {
                if (_generalSettings.DeletedRecommendations.Contains(book))
                {
                    _generalSettings.Recommendations.Remove(book);
                }
                else
                {
                    gridResult.Rows.Add(book.Key.Id, book.Key.Title, book.Key.Author, book.Value);
                }
            }
            btnDelete.Enabled = gridResult.SelectedRows.Count > 0;
        }

        private void gridResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnDelete.Enabled = gridResult.SelectedRows.Count > 0;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridResult.SelectedRows.Count > 0)
            {
                ExternalCatalogBook book = new ExternalCatalogBook(gridResult.SelectedRows[0].Cells[0].Value.ToString(),
                    gridResult.SelectedRows[0].Cells[1].Value.ToString(), gridResult.SelectedRows[0].Cells[2].Value.ToString(), null);
                CustomKeyValuePair<ExternalCatalogBook, double> k = new CustomKeyValuePair<ExternalCatalogBook, double>(book,
                    double.Parse(gridResult.SelectedRows[0].Cells[3].Value.ToString()));
                gridResult.Rows.RemoveAt(gridResult.SelectedRows[0].Index);
                btnDelete.Enabled = gridResult.SelectedRows.Count > 0;
                _generalSettings.Recommendations.Remove(k);
                _generalSettings.DeletedRecommendations.Add(k);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данный программный комплекс предназначен для \nформирования рекомендаций по комплектованию документного фонда \n\nАвтор: Кузнецов Антон Алексеевич",
                "О программе");
        }

        private void analyzeSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneralSettings settingsForm = new GeneralSettings(this);
            _hasDeleted = _generalSettings.DeletedRecommendations.Count > 0;
            settingsForm.SetSettings(_generalSettings);
            settingsForm.Show();
        }

        public void ClearDeleted()
        {
            if (_generalSettings.DeletedRecommendations.Count > 0)
            {
                _generalSettings.Recommendations.AddRange(_generalSettings.DeletedRecommendations);
                _generalSettings.DeletedRecommendations.Clear();
                _generalSettings.Recommendations = _generalSettings.Recommendations.OrderByDescending(pair => pair.Value).ToList();
                ShowResult(_generalSettings.Recommendations);
            }
        }

        public void UpdateGeneralSettings()
        {
            if (_hasDeleted && _generalSettings.DeletedRecommendations.Count == 0)
            {
                _generalSettings.Recommendations = _generalSettings.Recommendations.OrderByDescending(pair => pair.Value).ToList();
                ShowResult(_generalSettings.Recommendations);
            }
            int maximum = (int) (_generalSettings.UseMaxForAnalyze ? _generalSettings.MaxValueForAnalyze : 0);
            _progressDialog.SetAnalyzeMaximum(maximum);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Settings.GeneralSettings));

            using (FileStream fs = new FileStream(SETTINGS_PATH, FileMode.Create))
            {
                formatter.Serialize(fs, _generalSettings);
            }
        }

        private void externalCatalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExternalCatalogSettings externalCatalogSettings = new ExternalCatalogSettings();
            externalCatalogSettings.SetSettings(_generalSettings);
            externalCatalogSettings.Show();
        }
    }
}
