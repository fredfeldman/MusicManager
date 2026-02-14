namespace MusicManager
{
    public partial class Form1 : Form
    {
        private MusicDatabase musicDatabase;
        private MusicLibrary musicLibrary;
        private AudioPlayer audioPlayer;
        private MusicTrack? currentTrack;
        private bool isUserSeekingPosition = false;
        private AppSettings appSettings;

        public Form1()
        {
            InitializeComponent();

            appSettings = AppSettings.Load();
            musicDatabase = new MusicDatabase();
            musicLibrary = new MusicLibrary(musicDatabase);
            audioPlayer = new AudioPlayer();

            audioPlayer.PositionChanged += AudioPlayer_PositionChanged;
            audioPlayer.PlaybackStopped += AudioPlayer_PlaybackStopped;

            InitializeTreeView();
            UpdateTrackList();

            trackBarVolume.Value = appSettings.DefaultVolume;
            audioPlayer.SetVolume(trackBarVolume.Value);
        }

        private void InitializeTreeView()
        {
            treeView.Nodes.Clear();
            treeView.Nodes.Add("Library", "Music Library");
            treeView.Nodes.Add("Artists", "Artists");
            treeView.Nodes.Add("Albums", "Albums");
            treeView.Nodes.Add("Genres", "Genres");
            treeView.ExpandAll();
        }

        private void UpdateTreeView()
        {
            treeView.Nodes["Artists"]?.Nodes.Clear();
            treeView.Nodes["Albums"]?.Nodes.Clear();
            treeView.Nodes["Genres"]?.Nodes.Clear();

            foreach (var artist in musicLibrary.GetArtists())
            {
                if (!string.IsNullOrEmpty(artist))
                    treeView.Nodes["Artists"]?.Nodes.Add($"artist_{artist}", artist);
            }

            foreach (var album in musicLibrary.GetAlbums())
            {
                if (!string.IsNullOrEmpty(album))
                    treeView.Nodes["Albums"]?.Nodes.Add($"album_{album}", album);
            }

            foreach (var genre in musicLibrary.GetGenres())
            {
                if (!string.IsNullOrEmpty(genre))
                    treeView.Nodes["Genres"]?.Nodes.Add($"genre_{genre}", genre);
            }
        }

        private void UpdateTrackList(IEnumerable<MusicTrack>? tracks = null)
        {
            dataGridView.Rows.Clear();

            var tracksToDisplay = tracks ?? musicLibrary.Tracks;

            foreach (var track in tracksToDisplay)
            {
                dataGridView.Rows.Add(
                    track.Title,
                    track.Artist,
                    track.Album,
                    track.Genre,
                    track.Duration.ToString(@"mm\:ss")
                );
                dataGridView.Rows[dataGridView.Rows.Count - 1].Tag = track;
            }

            toolStripStatusLabel.Text = $"{dataGridView.Rows.Count} tracks";
        }

        private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a folder containing music files";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ScanFolder(dialog.SelectedPath);
                }
            }
        }

        private void ScanFolder(string folderPath)
        {
            toolStripStatusLabel.Text = "Scanning folder...";
            Application.DoEvents();

            var progress = new Progress<string>(msg =>
            {
                toolStripStatusLabel.Text = msg;
                Application.DoEvents();
            });

            var tracks = MusicScanner.ScanFolder(folderPath, progress);
            musicLibrary.AddTracks(tracks);
            musicDatabase.AddScanLocation(folderPath);

            UpdateTrackList();
            UpdateTreeView();
            toolStripStatusLabel.Text = $"Added {tracks.Count} tracks from {folderPath}";
        }

        private void addFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Audio Files|*.mp3;*.wav;*.wma;*.m4a;*.aac|All Files|*.*";
                dialog.Multiselect = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var file in dialog.FileNames)
                    {
                        var track = new MusicTrack
                        {
                            FilePath = file,
                            Title = Path.GetFileNameWithoutExtension(file),
                            Artist = "Unknown Artist",
                            Album = "Unknown Album",
                            Genre = "Unknown"
                        };
                        musicLibrary.AddTrack(track);
                    }

                    UpdateTrackList();
                    UpdateTreeView();
                    toolStripStatusLabel.Text = $"Added {dialog.FileNames.Length} files";
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var preferencesForm = new PreferencesForm(appSettings))
            {
                if (preferencesForm.ShowDialog() == DialogResult.OK)
                {
                    trackBarVolume.Value = appSettings.DefaultVolume;
                    audioPlayer.SetVolume(appSettings.DefaultVolume);
                    toolStripStatusLabel.Text = "Preferences saved";
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var searchResults = musicLibrary.SearchTracks(txtSearch.Text);
            UpdateTrackList(searchResults);
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;

            IEnumerable<MusicTrack> filteredTracks = musicLibrary.Tracks;

            if (e.Node.Name.StartsWith("artist_"))
            {
                var artist = e.Node.Text;
                filteredTracks = musicLibrary.Tracks.Where(t => t.Artist == artist);
            }
            else if (e.Node.Name.StartsWith("album_"))
            {
                var album = e.Node.Text;
                filteredTracks = musicLibrary.Tracks.Where(t => t.Album == album);
            }
            else if (e.Node.Name.StartsWith("genre_"))
            {
                var genre = e.Node.Text;
                filteredTracks = musicLibrary.Tracks.Where(t => t.Genre == genre);
            }

            UpdateTrackList(filteredTracks);
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView.Rows.Count)
            {
                var track = dataGridView.Rows[e.RowIndex].Tag as MusicTrack;
                if (track != null)
                {
                    PlayTrack(track);
                }
            }
        }

        private void PlayTrack(MusicTrack track)
        {
            try
            {
                currentTrack = track;
                audioPlayer.LoadFile(track.FilePath);
                audioPlayer.Play();

                lblCurrentTrack.Text = $"Now Playing: {track.Artist} - {track.Title}";
                lblDuration.Text = $"/ {audioPlayer.Duration:mm\\:ss}";
                trackBarPosition.Maximum = (int)audioPlayer.Duration.TotalSeconds;

                toolStripStatusLabel.Text = $"Playing: {track.Title}";

                musicLibrary.UpdatePlayCount(track.FilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing track: {ex.Message}", "Playback Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (currentTrack == null && dataGridView.SelectedRows.Count > 0)
            {
                var track = dataGridView.SelectedRows[0].Tag as MusicTrack;
                if (track != null)
                {
                    PlayTrack(track);
                    return;
                }
            }

            if (currentTrack != null)
            {
                audioPlayer.Play();
                toolStripStatusLabel.Text = "Playing";
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            audioPlayer.Pause();
            toolStripStatusLabel.Text = "Paused";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            audioPlayer.Stop();
            lblCurrentTrack.Text = "No track loaded";
            lblPosition.Text = "0:00";
            lblDuration.Text = "/ 0:00";
            trackBarPosition.Value = 0;
            toolStripStatusLabel.Text = "Stopped";
        }

        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
            audioPlayer.SetVolume(trackBarVolume.Value);
        }

        private void trackBarPosition_Scroll(object sender, EventArgs e)
        {
            if (!isUserSeekingPosition && audioPlayer.Duration > TimeSpan.Zero)
            {
                isUserSeekingPosition = true;
                var position = TimeSpan.FromSeconds(trackBarPosition.Value);
                audioPlayer.SetPosition(position);
                lblPosition.Text = position.ToString(@"mm\:ss");
                isUserSeekingPosition = false;
            }
        }

        private void AudioPlayer_PositionChanged(object? sender, TimeSpan position)
        {
            if (!isUserSeekingPosition)
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => UpdatePositionDisplay(position)));
                }
                else
                {
                    UpdatePositionDisplay(position);
                }
            }
        }

        private void UpdatePositionDisplay(TimeSpan position)
        {
            lblPosition.Text = position.ToString(@"mm\:ss");
            if (audioPlayer.Duration > TimeSpan.Zero)
            {
                trackBarPosition.Value = Math.Min((int)position.TotalSeconds, trackBarPosition.Maximum);
            }
        }

        private void AudioPlayer_PlaybackStopped(object? sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => 
                {
                    toolStripStatusLabel.Text = "Playback completed";
                }));
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            audioPlayer?.Dispose();
            musicDatabase?.Dispose();
        }

        private void scanDefaultFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(appSettings.DefaultMusicFolder))
            {
                MessageBox.Show("No default music folder is set. Please set one in Preferences.", 
                    "No Default Folder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Directory.Exists(appSettings.DefaultMusicFolder))
            {
                MessageBox.Show($"The default music folder does not exist:\n{appSettings.DefaultMusicFolder}", 
                    "Folder Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ScanFolder(appSettings.DefaultMusicFolder);
        }

        private void rescanLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "This will rescan all previously scanned locations. This may take some time. Continue?",
                "Rescan Library",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var locations = musicDatabase.GetScanLocations();
                if (locations.Count == 0)
                {
                    MessageBox.Show("No scan locations found. Please add folders first.", 
                        "No Locations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int totalTracksAdded = 0;
                foreach (var location in locations)
                {
                    if (Directory.Exists(location))
                    {
                        var progress = new Progress<string>(msg =>
                        {
                            toolStripStatusLabel.Text = msg;
                            Application.DoEvents();
                        });

                        var tracks = MusicScanner.ScanFolder(location, progress);
                        musicLibrary.AddTracks(tracks);
                        totalTracksAdded += tracks.Count;
                    }
                }

                UpdateTrackList();
                UpdateTreeView();
                toolStripStatusLabel.Text = $"Rescan complete. Processed {totalTracksAdded} tracks from {locations.Count} locations";
            }
        }

        private void cleanupMissingFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "This will remove tracks from the database whose files no longer exist. Continue?",
                "Cleanup Missing Files",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                toolStripStatusLabel.Text = "Cleaning up missing files...";
                Application.DoEvents();

                musicLibrary.RemoveMissingTracks();

                UpdateTrackList();
                UpdateTreeView();
                toolStripStatusLabel.Text = $"Cleanup complete. {musicDatabase.GetTrackCount()} tracks remaining";
            }
        }
    }
}
