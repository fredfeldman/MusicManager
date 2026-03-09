# Music Manager

> ⚠️ **Status: Early Development** — Features are incomplete and subject to change.

A Windows desktop application for managing and playing a local music collection, built with .NET 8 and Windows Forms.

## Features (Current)

- **Music Library** — Browse tracks organized by Artist, Album, and Genre in a tree view
- **SQLite Database** — Persistent storage of your music collection across sessions
- **Folder Scanning** — Scan a folder (and subfolders) to import music files into the library
- **Default Folder Scan** — One-click scan of a configured default music folder
- **Rescan Library** — Rescan all previously added locations to pick up new files
- **Cleanup Missing Files** — Remove database entries for files that no longer exist on disk
- **Search** — Filter the track list in real time by title, artist, album, or genre
- **Playback** — Play, pause, and stop tracks using the Windows MCI audio engine
- **Volume Control** — Adjust playback volume with a track bar
- **Position Seeking** — Scrub through a track using the position track bar
- **Preferences** — Configure default music folder, volume, and playback options

## Supported File Formats

| Format | Extension |
|--------|-----------|
| MP3    | `.mp3`    |
| WAV    | `.wav`    |
| WMA    | `.wma`    |
| MPEG-4 Audio | `.m4a` |
| AAC    | `.aac`    |

## Getting Started

### Prerequisites

- Windows 10 or later
- [.NET 8 Runtime](https://dotnet.microsoft.com/download/dotnet/8.0)

### Build from Source

```bash
git clone https://github.com/fredfeldman/MusicManager.git
cd MusicManager
dotnet build
dotnet run --project MusicManager
```

### First Use

1. Launch the application
2. Open **File → Preferences** and set your **Default Music Folder**
3. Click **File → Scan Default Folder** to import your music library
4. Double-click any track in the list to begin playback

## Project Structure

```
MusicManager/
├── Form1.cs / Form1.Designer.cs   # Main application window
├── MusicDatabase.cs               # SQLite database access layer
├── MusicLibrary.cs                # In-memory library with database integration
├── MusicScanner.cs                # Scans folders and creates MusicTrack objects
├── MusicTrack.cs                  # Track data model
├── AudioPlayer.cs                 # Playback via Windows MCI (winmm.dll)
├── PreferencesForm.cs             # Preferences dialog
├── AppSettings.cs                 # JSON-based settings persistence
└── MusicManager.csproj            # Project file (.NET 8, Windows Forms)
```

## Data Storage

| File | Location |
|------|----------|
| Database | `%AppData%\MusicManager\music.db` |
| Settings | `%AppData%\MusicManager\settings.json` |

## Dependencies

| Package | Version | Purpose |
|---------|---------|---------|
| [Microsoft.Data.Sqlite](https://www.nuget.org/packages/Microsoft.Data.Sqlite) | 10.0.3 | SQLite database access |

## Planned Features

- [ ] ID3 tag reading (title, artist, album, artwork from file metadata)
- [ ] Playlist creation and management
- [ ] Next / Previous track navigation
- [ ] Shuffle and repeat modes
- [ ] Album art display
- [ ] Play count and recently played views
- [ ] Drag-and-drop file import
- [ ] Export / import library data

## Known Limitations

- Track metadata (title, artist, album) is currently parsed from the filename only — full ID3 tag support is not yet implemented
- Playback uses the Windows MCI engine (`winmm.dll`) and is Windows-only
- No queue or playlist playback; tracks must be double-clicked individually

## License

This project does not yet have a license assigned. All rights reserved until further notice.
