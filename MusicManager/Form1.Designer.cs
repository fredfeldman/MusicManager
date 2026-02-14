namespace MusicManager
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            addFolderToolStripMenuItem = new ToolStripMenuItem();
            addFilesToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            splitContainer = new SplitContainer();
            treeView = new TreeView();
            dataGridView = new DataGridView();
            Title = new DataGridViewTextBoxColumn();
            Artist = new DataGridViewTextBoxColumn();
            Album = new DataGridViewTextBoxColumn();
            Genre = new DataGridViewTextBoxColumn();
            Duration = new DataGridViewTextBoxColumn();
            panelTop = new Panel();
            txtSearch = new TextBox();
            lblSearch = new Label();
            panelBottom = new Panel();
            lblCurrentTrack = new Label();
            trackBarPosition = new TrackBar();
            lblPosition = new Label();
            lblDuration = new Label();
            trackBarVolume = new TrackBar();
            lblVolume = new Label();
            btnStop = new Button();
            btnPause = new Button();
            btnPlay = new Button();
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            panelTop.SuspendLayout();
            panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarPosition).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarVolume).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1200, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addFolderToolStripMenuItem, addFilesToolStripMenuItem, toolStripSeparator1, preferencesToolStripMenuItem, toolStripSeparator2, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // addFolderToolStripMenuItem
            // 
            addFolderToolStripMenuItem.Name = "addFolderToolStripMenuItem";
            addFolderToolStripMenuItem.Size = new Size(180, 22);
            addFolderToolStripMenuItem.Text = "Add &Folder...";
            addFolderToolStripMenuItem.Click += addFolderToolStripMenuItem_Click;
            // 
            // addFilesToolStripMenuItem
            // 
            addFilesToolStripMenuItem.Name = "addFilesToolStripMenuItem";
            addFilesToolStripMenuItem.Size = new Size(180, 22);
            addFilesToolStripMenuItem.Text = "Add F&iles...";
            addFilesToolStripMenuItem.Click += addFilesToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(177, 6);
            // 
            // preferencesToolStripMenuItem
            // 
            preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            preferencesToolStripMenuItem.Size = new Size(180, 22);
            preferencesToolStripMenuItem.Text = "&Preferences...";
            preferencesToolStripMenuItem.Click += preferencesToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(177, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(180, 22);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 74);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(treeView);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(dataGridView);
            splitContainer.Size = new Size(1200, 446);
            splitContainer.SplitterDistance = 250;
            splitContainer.TabIndex = 1;
            // 
            // treeView
            // 
            treeView.Dock = DockStyle.Fill;
            treeView.Location = new Point(0, 0);
            treeView.Name = "treeView";
            treeView.Size = new Size(250, 446);
            treeView.TabIndex = 0;
            treeView.AfterSelect += treeView_AfterSelect;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { Title, Artist, Album, Genre, Duration });
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Location = new Point(0, 0);
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(946, 446);
            dataGridView.TabIndex = 0;
            dataGridView.CellDoubleClick += dataGridView_CellDoubleClick;
            // 
            // Title
            // 
            Title.HeaderText = "Title";
            Title.Name = "Title";
            Title.ReadOnly = true;
            Title.Width = 200;
            // 
            // Artist
            // 
            Artist.HeaderText = "Artist";
            Artist.Name = "Artist";
            Artist.ReadOnly = true;
            Artist.Width = 150;
            // 
            // Album
            // 
            Album.HeaderText = "Album";
            Album.Name = "Album";
            Album.ReadOnly = true;
            Album.Width = 150;
            // 
            // Genre
            // 
            Genre.HeaderText = "Genre";
            Genre.Name = "Genre";
            Genre.ReadOnly = true;
            // 
            // Duration
            // 
            Duration.HeaderText = "Duration";
            Duration.Name = "Duration";
            Duration.ReadOnly = true;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(txtSearch);
            panelTop.Controls.Add(lblSearch);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 24);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1200, 50);
            panelTop.TabIndex = 2;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.Location = new Point(68, 15);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(1120, 23);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(12, 18);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(45, 15);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Search:";
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(lblCurrentTrack);
            panelBottom.Controls.Add(trackBarPosition);
            panelBottom.Controls.Add(lblPosition);
            panelBottom.Controls.Add(lblDuration);
            panelBottom.Controls.Add(trackBarVolume);
            panelBottom.Controls.Add(lblVolume);
            panelBottom.Controls.Add(btnStop);
            panelBottom.Controls.Add(btnPause);
            panelBottom.Controls.Add(btnPlay);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 520);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(1200, 120);
            panelBottom.TabIndex = 3;
            // 
            // lblCurrentTrack
            // 
            lblCurrentTrack.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblCurrentTrack.Location = new Point(12, 10);
            lblCurrentTrack.Name = "lblCurrentTrack";
            lblCurrentTrack.Size = new Size(1176, 20);
            lblCurrentTrack.TabIndex = 8;
            lblCurrentTrack.Text = "No track loaded";
            lblCurrentTrack.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // trackBarPosition
            // 
            trackBarPosition.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            trackBarPosition.Location = new Point(12, 33);
            trackBarPosition.Maximum = 1000;
            trackBarPosition.Name = "trackBarPosition";
            trackBarPosition.Size = new Size(1020, 45);
            trackBarPosition.TabIndex = 7;
            trackBarPosition.TickFrequency = 50;
            trackBarPosition.Scroll += trackBarPosition_Scroll;
            // 
            // lblPosition
            // 
            lblPosition.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPosition.Location = new Point(1038, 38);
            lblPosition.Name = "lblPosition";
            lblPosition.Size = new Size(70, 23);
            lblPosition.TabIndex = 6;
            lblPosition.Text = "0:00";
            lblPosition.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblDuration
            // 
            lblDuration.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDuration.Location = new Point(1114, 38);
            lblDuration.Name = "lblDuration";
            lblDuration.Size = new Size(70, 23);
            lblDuration.TabIndex = 5;
            lblDuration.Text = "/ 0:00";
            lblDuration.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // trackBarVolume
            // 
            trackBarVolume.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            trackBarVolume.Location = new Point(1048, 72);
            trackBarVolume.Maximum = 100;
            trackBarVolume.Name = "trackBarVolume";
            trackBarVolume.Size = new Size(140, 45);
            trackBarVolume.TabIndex = 4;
            trackBarVolume.TickFrequency = 10;
            trackBarVolume.Value = 50;
            trackBarVolume.Scroll += trackBarVolume_Scroll;
            // 
            // lblVolume
            // 
            lblVolume.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblVolume.AutoSize = true;
            lblVolume.Location = new Point(990, 82);
            lblVolume.Name = "lblVolume";
            lblVolume.Size = new Size(50, 15);
            lblVolume.TabIndex = 3;
            lblVolume.Text = "Volume:";
            // 
            // btnStop
            // 
            btnStop.Location = new Point(174, 78);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(75, 30);
            btnStop.TabIndex = 2;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnPause
            // 
            btnPause.Location = new Point(93, 78);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(75, 30);
            btnPause.TabIndex = 1;
            btnPause.Text = "Pause";
            btnPause.UseVisualStyleBackColor = true;
            btnPause.Click += btnPause_Click;
            // 
            // btnPlay
            // 
            btnPlay.Location = new Point(12, 78);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(75, 30);
            btnPlay.TabIndex = 0;
            btnPlay.Text = "Play";
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += btnPlay_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel });
            statusStrip.Location = new Point(0, 640);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1200, 22);
            statusStrip.TabIndex = 4;
            statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new Size(39, 17);
            toolStripStatusLabel.Text = "Ready";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 662);
            Controls.Add(splitContainer);
            Controls.Add(panelTop);
            Controls.Add(panelBottom);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "Form1";
            Text = "Music Manager";
            FormClosing += Form1_FormClosing;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelBottom.ResumeLayout(false);
            panelBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarPosition).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarVolume).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem addFolderToolStripMenuItem;
        private ToolStripMenuItem addFilesToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem preferencesToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem exitToolStripMenuItem;
        private SplitContainer splitContainer;
        private TreeView treeView;
        private DataGridView dataGridView;
        private Panel panelTop;
        private TextBox txtSearch;
        private Label lblSearch;
        private Panel panelBottom;
        private Button btnPlay;
        private Button btnPause;
        private Button btnStop;
        private TrackBar trackBarVolume;
        private Label lblVolume;
        private Label lblDuration;
        private Label lblPosition;
        private TrackBar trackBarPosition;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel;
        private DataGridViewTextBoxColumn Title;
        private DataGridViewTextBoxColumn Artist;
        private DataGridViewTextBoxColumn Album;
        private DataGridViewTextBoxColumn Genre;
        private DataGridViewTextBoxColumn Duration;
        private Label lblCurrentTrack;
    }
}
