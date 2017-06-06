using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECatalogRecommendations.Models;

namespace ECatalogRecommendations
{
    public partial class ExternalCatalogSettings : Form
    {
        private Settings.GeneralSettings _settings;

        public ExternalCatalogSettings()
        {
            InitializeComponent();
        }

        public void SetSettings(Settings.GeneralSettings settings)
        {
            _settings = settings;
            rbDatabase.Checked = _settings.ExternalCatalogType == Constants.USE_DATABASE;
            rbXml.Checked = _settings.ExternalCatalogType == Constants.USE_XML;
            txtPath.Text = _settings.ExternalCatalogPath;

            lblPath.Enabled = rbXml.Checked;
            txtPath.Enabled = rbXml.Checked;
            btnPath.Enabled = rbXml.Checked;
        }

        private void setPathEnabled(bool enabled)
        {
            lblPath.Enabled = enabled;
            txtPath.Enabled = enabled;
            btnPath.Enabled = enabled;
        }

        private void rbDatabase_CheckedChanged(object sender, EventArgs e)
        {
            setPathEnabled(rbXml.Checked);
        }

        private void rbXml_CheckedChanged(object sender, EventArgs e)
        {
            setPathEnabled(rbXml.Checked);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (rbDatabase.Checked)
            {
                _settings.ExternalCatalogType = Constants.USE_DATABASE;
            }
            else if (rbXml.Checked)
            {              
                if (File.Exists(txtPath.Text))
                {
                    _settings.ExternalCatalogPath = txtPath.Text;
                    _settings.ExternalCatalogType = Constants.USE_XML;
                }
                else
                {
                    MessageBox.Show("Выбранный файл не существует", "Ошибка");
                    return;
                }
            }    
            Close();
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "xml-файлы (*.xml)|*.xml";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = dialog.FileName;
            }
        }
    }
}
