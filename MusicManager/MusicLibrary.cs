using System.Collections.ObjectModel;

namespace MusicManager
{
    public class MusicLibrary
    {
        public ObservableCollection<MusicTrack> Tracks { get; set; }
        public List<string> Playlists { get; set; }

        public MusicLibrary()
        {
            Tracks = new ObservableCollection<MusicTrack>();
            Playlists = new List<string>();
        }

        public void AddTrack(MusicTrack track)
        {
            Tracks.Add(track);
        }

        public void RemoveTrack(MusicTrack track)
        {
            Tracks.Remove(track);
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
    }
}
