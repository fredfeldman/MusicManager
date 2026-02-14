namespace MusicManager
{
    public class MusicTrack
    {
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
        public int Year { get; set; }
        public int TrackNumber { get; set; }
        public byte[]? AlbumArt { get; set; }

        public override string ToString()
        {
            return $"{Artist} - {Title}";
        }
    }
}
