using System;
using System.Windows.Forms;

namespace ECatalogRecommendations
{
    public partial class GeneralSettings : Form
    {
        private Settings.GeneralSettings _generalSettings;
        private MainForm _parent;

        public GeneralSettings(MainForm parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        public void SetSettings(Settings.GeneralSettings settings)
        {
            _generalSettings = settings;
            int count = _generalSettings.DeletedRecommendations.Count;
            btnClearDeleted.Enabled = count > 0;
            lblDeleted.Text = count.ToString();
            checkUseMaxForAnalyze.Checked = _generalSettings.UseMaxForAnalyze;
            setMaxAvailable(_generalSettings.UseMaxForAnalyze);
            maxForAnalyze.Value = _generalSettings.MaxValueForAnalyze;
        }

        private void setMaxAvailable(bool isAvailable)
        {
            lblUseMax.Enabled = isAvailable;
            maxForAnalyze.Enabled = isAvailable;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _generalSettings.UseMaxForAnalyze = checkUseMaxForAnalyze.Checked;
            _generalSettings.MaxValueForAnalyze = maxForAnalyze.Value;
            _parent.UpdateGeneralSettings();
            Close();
        }

        private void checkUseMaxForAnalyze_CheckedChanged(object sender, EventArgs e)
        {
            setMaxAvailable(checkUseMaxForAnalyze.Checked);
        }

        private void btnClearDeleted_Click(object sender, EventArgs e)
        {
            _parent.ClearDeleted();
            int count = _generalSettings.DeletedRecommendations.Count;
            lblDeleted.Text = count.ToString();
            btnClearDeleted.Enabled = count > 0;
        }
    }
}
