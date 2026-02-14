using System.Runtime.InteropServices;
using System.Text;

namespace MusicManager
{
    public class AudioPlayer : IDisposable
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string command, StringBuilder returnValue, int returnLength, IntPtr hwndCallback);

        private string? currentFile;
        private bool isPlaying;
        private System.Windows.Forms.Timer? positionTimer;

        public event EventHandler<TimeSpan>? PositionChanged;
        public event EventHandler? PlaybackStopped;

        public bool IsPlaying => isPlaying;
        public TimeSpan Duration { get; private set; }
        public TimeSpan Position { get; private set; }

        public AudioPlayer()
        {
            positionTimer = new System.Windows.Forms.Timer();
            positionTimer.Interval = 500;
            positionTimer.Tick += PositionTimer_Tick;
        }

        private void PositionTimer_Tick(object? sender, EventArgs e)
        {
            if (isPlaying)
            {
                UpdatePosition();
            }
        }

        public void LoadFile(string filePath)
        {
            Stop();
            currentFile = filePath;

            mciSendString($"open \"{filePath}\" type mpegvideo alias MediaFile", null!, 0, IntPtr.Zero);
            
            StringBuilder lengthBuf = new StringBuilder(128);
            mciSendString("status MediaFile length", lengthBuf, 128, IntPtr.Zero);
            
            if (int.TryParse(lengthBuf.ToString(), out int length))
            {
                Duration = TimeSpan.FromMilliseconds(length);
            }
        }

        public void Play()
        {
            if (currentFile == null) return;

            mciSendString("play MediaFile", null!, 0, IntPtr.Zero);
            isPlaying = true;
            positionTimer?.Start();
        }

        public void Pause()
        {
            mciSendString("pause MediaFile", null!, 0, IntPtr.Zero);
            isPlaying = false;
            positionTimer?.Stop();
        }

        public void Stop()
        {
            mciSendString("stop MediaFile", null!, 0, IntPtr.Zero);
            mciSendString("close MediaFile", null!, 0, IntPtr.Zero);
            isPlaying = false;
            Position = TimeSpan.Zero;
            positionTimer?.Stop();
            PlaybackStopped?.Invoke(this, EventArgs.Empty);
        }

        public void SetPosition(TimeSpan position)
        {
            long milliseconds = (long)position.TotalMilliseconds;
            mciSendString($"seek MediaFile to {milliseconds}", null!, 0, IntPtr.Zero);
            if (isPlaying)
            {
                mciSendString("play MediaFile", null!, 0, IntPtr.Zero);
            }
            Position = position;
        }

        public void SetVolume(int volume)
        {
            if (volume < 0) volume = 0;
            if (volume > 100) volume = 100;
            
            int mciVolume = (volume * 10);
            mciSendString($"setaudio MediaFile volume to {mciVolume}", null!, 0, IntPtr.Zero);
        }

        private void UpdatePosition()
        {
            StringBuilder posBuf = new StringBuilder(128);
            mciSendString("status MediaFile position", posBuf, 128, IntPtr.Zero);
            
            if (int.TryParse(posBuf.ToString(), out int pos))
            {
                Position = TimeSpan.FromMilliseconds(pos);
                PositionChanged?.Invoke(this, Position);

                if (Position >= Duration && Duration > TimeSpan.Zero)
                {
                    Stop();
                }
            }
        }

        public void Dispose()
        {
            Stop();
            positionTimer?.Dispose();
        }
    }
}
