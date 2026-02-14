namespace MusicManager
{
    partial class PreferencesForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            groupBoxLibrary = new GroupBox();
            btnBrowse = new Button();
            txtDefaultMusicFolder = new TextBox();
            lblDefaultMusicFolder = new Label();
            groupBoxPlayback = new GroupBox();
            numDefaultVolume = new NumericUpDown();
            lblDefaultVolume = new Label();
            chkRememberLastPosition = new CheckBox();
            btnOK = new Button();
            btnCancel = new Button();
            groupBoxLibrary.SuspendLayout();
            groupBoxPlayback.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDefaultVolume).BeginInit();
            SuspendLayout();
            // 
            // groupBoxLibrary
            // 
            groupBoxLibrary.Controls.Add(btnBrowse);
            groupBoxLibrary.Controls.Add(txtDefaultMusicFolder);
            groupBoxLibrary.Controls.Add(lblDefaultMusicFolder);
            groupBoxLibrary.Location = new Point(12, 12);
            groupBoxLibrary.Name = "groupBoxLibrary";
            groupBoxLibrary.Size = new Size(560, 100);
            groupBoxLibrary.TabIndex = 0;
            groupBoxLibrary.TabStop = false;
            groupBoxLibrary.Text = "Library";
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(470, 51);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 2;
            btnBrowse.Text = "Browse...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // txtDefaultMusicFolder
            // 
            txtDefaultMusicFolder.Location = new Point(15, 51);
            txtDefaultMusicFolder.Name = "txtDefaultMusicFolder";
            txtDefaultMusicFolder.Size = new Size(449, 23);
            txtDefaultMusicFolder.TabIndex = 1;
            // 
            // lblDefaultMusicFolder
            // 
            lblDefaultMusicFolder.AutoSize = true;
            lblDefaultMusicFolder.Location = new Point(15, 30);
            lblDefaultMusicFolder.Name = "lblDefaultMusicFolder";
            lblDefaultMusicFolder.Size = new Size(126, 15);
            lblDefaultMusicFolder.TabIndex = 0;
            lblDefaultMusicFolder.Text = "Default Music Folder:";
            // 
            // groupBoxPlayback
            // 
            groupBoxPlayback.Controls.Add(numDefaultVolume);
            groupBoxPlayback.Controls.Add(lblDefaultVolume);
            groupBoxPlayback.Controls.Add(chkRememberLastPosition);
            groupBoxPlayback.Location = new Point(12, 118);
            groupBoxPlayback.Name = "groupBoxPlayback";
            groupBoxPlayback.Size = new Size(560, 120);
            groupBoxPlayback.TabIndex = 1;
            groupBoxPlayback.TabStop = false;
            groupBoxPlayback.Text = "Playback";
            // 
            // numDefaultVolume
            // 
            numDefaultVolume.Location = new Point(130, 56);
            numDefaultVolume.Name = "numDefaultVolume";
            numDefaultVolume.Size = new Size(80, 23);
            numDefaultVolume.TabIndex = 2;
            numDefaultVolume.Value = 50;
            // 
            // lblDefaultVolume
            // 
            lblDefaultVolume.AutoSize = true;
            lblDefaultVolume.Location = new Point(15, 58);
            lblDefaultVolume.Name = "lblDefaultVolume";
            lblDefaultVolume.Size = new Size(95, 15);
            lblDefaultVolume.TabIndex = 1;
            lblDefaultVolume.Text = "Default Volume:";
            // 
            // chkRememberLastPosition
            // 
            chkRememberLastPosition.AutoSize = true;
            chkRememberLastPosition.Checked = true;
            chkRememberLastPosition.CheckState = CheckState.Checked;
            chkRememberLastPosition.Location = new Point(15, 30);
            chkRememberLastPosition.Name = "chkRememberLastPosition";
            chkRememberLastPosition.Size = new Size(212, 19);
            chkRememberLastPosition.TabIndex = 0;
            chkRememberLastPosition.Text = "Remember last playback position";
            chkRememberLastPosition.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(416, 254);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 30);
            btnOK.TabIndex = 2;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(497, 254);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 30);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // PreferencesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 296);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(groupBoxPlayback);
            Controls.Add(groupBoxLibrary);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PreferencesForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Preferences";
            groupBoxLibrary.ResumeLayout(false);
            groupBoxLibrary.PerformLayout();
            groupBoxPlayback.ResumeLayout(false);
            groupBoxPlayback.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDefaultVolume).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxLibrary;
        private Button btnBrowse;
        private TextBox txtDefaultMusicFolder;
        private Label lblDefaultMusicFolder;
        private GroupBox groupBoxPlayback;
        private NumericUpDown numDefaultVolume;
        private Label lblDefaultVolume;
        private CheckBox chkRememberLastPosition;
        private Button btnOK;
        private Button btnCancel;
    }
}
