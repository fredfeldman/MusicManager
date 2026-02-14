namespace MusicManager
{
    public partial class PreferencesForm : Form
    {
        private AppSettings settings;

        public PreferencesForm(AppSettings settings)
        {
            InitializeComponent();
            this.settings = settings;
            LoadSettings();
        }

        private void LoadSettings()
        {
            txtDefaultMusicFolder.Text = settings.DefaultMusicFolder;
            numDefaultVolume.Value = settings.DefaultVolume;
            chkRememberLastPosition.Checked = settings.RememberLastPosition;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select the default music folder";
                
                if (!string.IsNullOrEmpty(settings.DefaultMusicFolder) && 
                    Directory.Exists(settings.DefaultMusicFolder))
                {
                    dialog.SelectedPath = settings.DefaultMusicFolder;
                }

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtDefaultMusicFolder.Text = dialog.SelectedPath;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            settings.DefaultMusicFolder = txtDefaultMusicFolder.Text;
            settings.DefaultVolume = (int)numDefaultVolume.Value;
            settings.RememberLastPosition = chkRememberLastPosition.Checked;

            try
            {
                settings.Save();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
