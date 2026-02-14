using Microsoft.Data.Sqlite;

namespace MusicManager
{
    public class MusicDatabase : IDisposable
    {
        private readonly string connectionString;
        private SqliteConnection? connection;

        public MusicDatabase()
        {
            var dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "MusicManager",
                "music.db");

            var directory = Path.GetDirectoryName(dbPath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            connectionString = $"Data Source={dbPath}";
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Tracks (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FilePath TEXT NOT NULL UNIQUE,
                    Title TEXT NOT NULL,
                    Artist TEXT,
                    Album TEXT,
                    Genre TEXT,
                    Year INTEGER,
                    TrackNumber INTEGER,
                    Duration INTEGER,
                    DateAdded TEXT NOT NULL,
                    LastPlayed TEXT,
                    PlayCount INTEGER DEFAULT 0
                );

                CREATE INDEX IF NOT EXISTS idx_tracks_artist ON Tracks(Artist);
                CREATE INDEX IF NOT EXISTS idx_tracks_album ON Tracks(Album);
                CREATE INDEX IF NOT EXISTS idx_tracks_genre ON Tracks(Genre);
                CREATE INDEX IF NOT EXISTS idx_tracks_filepath ON Tracks(FilePath);

                CREATE TABLE IF NOT EXISTS Playlists (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL UNIQUE,
                    DateCreated TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS PlaylistTracks (
                    PlaylistId INTEGER NOT NULL,
                    TrackId INTEGER NOT NULL,
                    Position INTEGER NOT NULL,
                    FOREIGN KEY (PlaylistId) REFERENCES Playlists(Id) ON DELETE CASCADE,
                    FOREIGN KEY (TrackId) REFERENCES Tracks(Id) ON DELETE CASCADE,
                    PRIMARY KEY (PlaylistId, TrackId)
                );

                CREATE TABLE IF NOT EXISTS ScanLocations (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FolderPath TEXT NOT NULL UNIQUE,
                    LastScanned TEXT
                );
            ";
            command.ExecuteNonQuery();
        }

        public void AddOrUpdateTrack(MusicTrack track)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Tracks (FilePath, Title, Artist, Album, Genre, Year, TrackNumber, Duration, DateAdded)
                VALUES (@FilePath, @Title, @Artist, @Album, @Genre, @Year, @TrackNumber, @Duration, @DateAdded)
                ON CONFLICT(FilePath) DO UPDATE SET
                    Title = @Title,
                    Artist = @Artist,
                    Album = @Album,
                    Genre = @Genre,
                    Year = @Year,
                    TrackNumber = @TrackNumber,
                    Duration = @Duration
            ";

            command.Parameters.AddWithValue("@FilePath", track.FilePath);
            command.Parameters.AddWithValue("@Title", track.Title ?? string.Empty);
            command.Parameters.AddWithValue("@Artist", track.Artist ?? string.Empty);
            command.Parameters.AddWithValue("@Album", track.Album ?? string.Empty);
            command.Parameters.AddWithValue("@Genre", track.Genre ?? string.Empty);
            command.Parameters.AddWithValue("@Year", track.Year);
            command.Parameters.AddWithValue("@TrackNumber", track.TrackNumber);
            command.Parameters.AddWithValue("@Duration", (long)track.Duration.TotalSeconds);
            command.Parameters.AddWithValue("@DateAdded", DateTime.Now.ToString("o"));

            command.ExecuteNonQuery();
        }

        public void AddTracks(IEnumerable<MusicTrack> tracks)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();
            try
            {
                foreach (var track in tracks)
                {
                    var command = connection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandText = @"
                        INSERT INTO Tracks (FilePath, Title, Artist, Album, Genre, Year, TrackNumber, Duration, DateAdded)
                        VALUES (@FilePath, @Title, @Artist, @Album, @Genre, @Year, @TrackNumber, @Duration, @DateAdded)
                        ON CONFLICT(FilePath) DO UPDATE SET
                            Title = @Title,
                            Artist = @Artist,
                            Album = @Album,
                            Genre = @Genre,
                            Year = @Year,
                            TrackNumber = @TrackNumber,
                            Duration = @Duration
                    ";

                    command.Parameters.AddWithValue("@FilePath", track.FilePath);
                    command.Parameters.AddWithValue("@Title", track.Title ?? string.Empty);
                    command.Parameters.AddWithValue("@Artist", track.Artist ?? string.Empty);
                    command.Parameters.AddWithValue("@Album", track.Album ?? string.Empty);
                    command.Parameters.AddWithValue("@Genre", track.Genre ?? string.Empty);
                    command.Parameters.AddWithValue("@Year", track.Year);
                    command.Parameters.AddWithValue("@TrackNumber", track.TrackNumber);
                    command.Parameters.AddWithValue("@Duration", (long)track.Duration.TotalSeconds);
                    command.Parameters.AddWithValue("@DateAdded", DateTime.Now.ToString("o"));

                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public List<MusicTrack> GetAllTracks()
        {
            var tracks = new List<MusicTrack>();

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT FilePath, Title, Artist, Album, Genre, Year, TrackNumber, Duration
                FROM Tracks
                ORDER BY Artist, Album, TrackNumber, Title
            ";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                tracks.Add(new MusicTrack
                {
                    FilePath = reader.GetString(0),
                    Title = reader.GetString(1),
                    Artist = reader.GetString(2),
                    Album = reader.GetString(3),
                    Genre = reader.GetString(4),
                    Year = reader.GetInt32(5),
                    TrackNumber = reader.GetInt32(6),
                    Duration = TimeSpan.FromSeconds(reader.GetInt64(7))
                });
            }

            return tracks;
        }

        public void RemoveTrack(string filePath)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Tracks WHERE FilePath = @FilePath";
            command.Parameters.AddWithValue("@FilePath", filePath);
            command.ExecuteNonQuery();
        }

        public void RemoveMissingTracks()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT FilePath FROM Tracks";

            var tracksToRemove = new List<string>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var filePath = reader.GetString(0);
                    if (!File.Exists(filePath))
                    {
                        tracksToRemove.Add(filePath);
                    }
                }
            }

            foreach (var filePath in tracksToRemove)
            {
                RemoveTrack(filePath);
            }
        }

        public void UpdatePlayCount(string filePath)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE Tracks 
                SET PlayCount = PlayCount + 1, LastPlayed = @LastPlayed
                WHERE FilePath = @FilePath
            ";
            command.Parameters.AddWithValue("@FilePath", filePath);
            command.Parameters.AddWithValue("@LastPlayed", DateTime.Now.ToString("o"));
            command.ExecuteNonQuery();
        }

        public void AddScanLocation(string folderPath)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO ScanLocations (FolderPath, LastScanned)
                VALUES (@FolderPath, @LastScanned)
                ON CONFLICT(FolderPath) DO UPDATE SET LastScanned = @LastScanned
            ";
            command.Parameters.AddWithValue("@FolderPath", folderPath);
            command.Parameters.AddWithValue("@LastScanned", DateTime.Now.ToString("o"));
            command.ExecuteNonQuery();
        }

        public List<string> GetScanLocations()
        {
            var locations = new List<string>();

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT FolderPath FROM ScanLocations";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                locations.Add(reader.GetString(0));
            }

            return locations;
        }

        public int GetTrackCount()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM Tracks";
            return Convert.ToInt32(command.ExecuteScalar());
        }

        public void Dispose()
        {
            connection?.Dispose();
        }
    }
}
