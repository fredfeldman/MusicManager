namespace MusicManager
{
    public class MusicScanner
    {
        private static readonly string[] SupportedExtensions = { ".mp3", ".wav", ".wma", ".m4a", ".aac" };

        public static List<MusicTrack> ScanFolder(string folderPath, IProgress<string>? progress = null)
        {
            List<MusicTrack> tracks = new List<MusicTrack>();

            if (!Directory.Exists(folderPath))
                return tracks;

            try
            {
                var files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)
                    .Where(f => SupportedExtensions.Contains(Path.GetExtension(f).ToLower()));

                foreach (var file in files)
                {
                    progress?.Report($"Scanning: {Path.GetFileName(file)}");
                    
                    try
                    {
                        var track = CreateTrackFromFile(file);
                        if (track != null)
                        {
                            tracks.Add(track);
                        }
                    }
                    catch
                    {
                        // Skip files that can't be read
                    }
                }
            }
            catch (Exception ex)
            {
                progress?.Report($"Error scanning folder: {ex.Message}");
            }

            return tracks;
        }

        private static MusicTrack? CreateTrackFromFile(string filePath)
        {
            try
            {
                // For now, create basic track info from filename
                // In a full implementation, you'd use TagLib# or similar to read ID3 tags
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                
                var track = new MusicTrack
                {
                    FilePath = filePath,
                    Title = fileName,
                    Artist = "Unknown Artist",
                    Album = "Unknown Album",
                    Genre = "Unknown",
                    Duration = TimeSpan.Zero // Would need to read from file
                };

                // Try to parse "Artist - Title" format
                if (fileName.Contains(" - "))
                {
                    var parts = fileName.Split(new[] { " - " }, 2, StringSplitOptions.None);
                    track.Artist = parts[0].Trim();
                    track.Title = parts[1].Trim();
                }

                return track;
            }
            catch
            {
                return null;
            }
        }
    }
}
