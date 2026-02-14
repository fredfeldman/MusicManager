# Music Manager Database Implementation

## Overview
The Music Manager application now includes a SQLite database for persistent storage of your music collection. This allows tracks to be saved between sessions and provides efficient querying and management capabilities.

## Features Implemented

### 1. Database Storage (MusicDatabase.cs)
- **SQLite Database**: Located in `%AppData%\MusicManager\music.db`
- **Tracks Table**: Stores all music track information including:
  - File path, title, artist, album, genre
  - Year, track number, duration
  - Date added, last played, play count
- **Playlists Table**: Ready for future playlist functionality
- **ScanLocations Table**: Tracks folders that have been scanned
- **Indexes**: Optimized for searching by artist, album, genre, and file path

### 2. New Menu Options

#### File Menu
- **Add Folder**: Manually select a folder to scan for music files
- **Add Files**: Manually select individual music files to add
- **Scan Default Folder**: Automatically scans the default music folder set in Preferences
- **Rescan Library**: Rescans all previously added folder locations
- **Cleanup Missing Files**: Removes database entries for files that no longer exist

### 3. Default Folder Scanning
To use the "Scan Default Folder" feature:
1. Go to **File → Preferences**
2. Set your **Default Music Folder** (e.g., `C:\Users\YourName\Music`)
3. Click **File → Scan Default Folder** to automatically scan that location

### 4. Persistent Storage
- All tracks are automatically saved to the database when added
- Track play counts and last played times are recorded
- Scan locations are remembered for easy rescanning
- Data persists between application sessions

### 5. Database Operations

#### Adding Tracks
```csharp
// Tracks are automatically saved when using:
musicLibrary.AddTrack(track);        // Single track
musicLibrary.AddTracks(tracks);      // Multiple tracks (uses transaction)
```

#### Loading Tracks
- Tracks are automatically loaded from the database when the application starts
- The database is queried on startup in `MusicLibrary` constructor

#### Updating Track Information
- Track information is updated in the database when:
  - A track is played (increments play count)
  - File metadata is re-scanned

#### Removing Tracks
- Use **File → Cleanup Missing Files** to remove tracks whose files no longer exist
- Individual tracks can be removed via the library (automatically removes from DB)

## Database Schema

### Tracks Table
```sql
CREATE TABLE Tracks (
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
```

### ScanLocations Table
```sql
CREATE TABLE ScanLocations (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FolderPath TEXT NOT NULL UNIQUE,
    LastScanned TEXT
);
```

## Usage Tips

1. **Initial Setup**: 
   - Set your default music folder in Preferences
   - Use "Scan Default Folder" to populate your library

2. **Adding More Music**:
   - Use "Add Folder" to scan additional directories
   - All scanned locations are remembered

3. **Keeping Library Updated**:
   - Use "Rescan Library" to update tracks from all previously scanned folders
   - Use "Cleanup Missing Files" periodically to remove orphaned entries

4. **Performance**:
   - Database uses indexes for fast searching
   - Batch operations use transactions for better performance
   - Large libraries are supported efficiently

## Technical Details

### Dependencies
- **Microsoft.Data.Sqlite** (version 10.0.3): Official Microsoft SQLite provider for .NET

### File Locations
- **Database**: `%AppData%\MusicManager\music.db`
- **Settings**: `%AppData%\MusicManager\settings.json`

### Classes Modified
- `Form1.cs`: Added menu handlers and scan functionality
- `MusicLibrary.cs`: Integrated with database for CRUD operations
- `MusicDatabase.cs`: New class handling all database operations

### Error Handling
- Database operations are wrapped in try-catch blocks
- Missing folders are handled gracefully with user notifications
- File access errors during scanning are logged and skipped

## Future Enhancements
- Playlist management (database tables already created)
- Advanced search and filtering
- Statistics and reporting (play counts, most played, etc.)
- Album art storage
- Smart playlists based on criteria
- Import/export functionality
