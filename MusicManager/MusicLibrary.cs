using System.Collections.ObjectModel;

namespace MusicManager
{
    public class MusicLibrary
    {
        public ObservableCollection<MusicTrack> Tracks { get; set; }
        public List<string> Playlists { get; set; }
        private readonly MusicDatabase database;

        public MusicLibrary(MusicDatabase db)
        {
            database = db;
            Tracks = new ObservableCollection<MusicTrack>();
            Playlists = new List<string>();
            LoadTracksFromDatabase();
        }

        public void LoadTracksFromDatabase()
        {
            Tracks.Clear();
            var tracks = database.GetAllTracks();
            foreach (var track in tracks)
            {
                Tracks.Add(track);
            }
        }

        public void AddTrack(MusicTrack track)
        {
            database.AddOrUpdateTrack(track);

            var existingTrack = Tracks.FirstOrDefault(t => t.FilePath == track.FilePath);
            if (existingTrack != null)
            {
                Tracks.Remove(existingTrack);
            }
            Tracks.Add(track);
        }

        public void AddTracks(IEnumerable<MusicTrack> tracks)
        {
            database.AddTracks(tracks);

            foreach (var track in tracks)
            {
                var existingTrack = Tracks.FirstOrDefault(t => t.FilePath == track.FilePath);
                if (existingTrack != null)
                {
                    Tracks.Remove(existingTrack);
                }
                Tracks.Add(track);
            }
        }

        public void RemoveTrack(MusicTrack track)
        {
            database.RemoveTrack(track.FilePath);
            Tracks.Remove(track);
        }

        public void RemoveMissingTracks()
        {
            database.RemoveMissingTracks();
            LoadTracksFromDatabase();
        }

        public IEnumerable<MusicTrack> SearchTracks(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return Tracks;

            searchText = searchText.ToLower();
            return Tracks.Where(t =>
                t.Title.ToLower().Contains(searchText) ||
                t.Artist.ToLower().Contains(searchText) ||
                t.Album.ToLower().Contains(searchText) ||
                t.Genre.ToLower().Contains(searchText));
        }

        public IEnumerable<string> GetArtists()
        {
            return Tracks.Select(t => t.Artist).Distinct().OrderBy(a => a);
        }

        public IEnumerable<string> GetAlbums()
        {
            return Tracks.Select(t => t.Album).Distinct().OrderBy(a => a);
        }

        public IEnumerable<string> GetGenres()
        {
            return Tracks.Select(t => t.Genre).Distinct().OrderBy(g => g);
        }

        public void UpdatePlayCount(string filePath)
        {
            database.UpdatePlayCount(filePath);
        }
    }
}
