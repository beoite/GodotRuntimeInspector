namespace GodotRuntimeInspector.Scripts
{
    public class MyLog
    {
        public const string EnableFileLogging = "debug/file_logging/enable_file_logging";
        public const string FileLogPath = "debug/file_logging/log_path";
        public Godot.Variant VariantLogPath = new Godot.Variant();
        public string LogPath = string.Empty;
        public string LogData = string.Empty;
        public System.IO.FileSystemWatcher? FileSystemWatcher = null;
        public uint ReadCounter = 0;
        public uint WriteCounter = 0;
        public int SecondsPerRead = 5;
        public double LastLogRead = 0;
        public double TimeSinceLastLogRead = 0;

        public MyLog()
        {
            Godot.ProjectSettings.SetSetting(EnableFileLogging, true);
            VariantLogPath = Godot.ProjectSettings.GetSetting(FileLogPath);
            LogPath = Godot.ProjectSettings.GlobalizePath(VariantLogPath.AsString());

            string? directoryPath = System.IO.Path.GetDirectoryName(LogPath);
            if (directoryPath == null)
            {
                return;
            }

            FileSystemWatcher = new System.IO.FileSystemWatcher(directoryPath);
            FileSystemWatcher.NotifyFilter = System.IO.NotifyFilters.Attributes
                                            | System.IO.NotifyFilters.CreationTime
                                            | System.IO.NotifyFilters.DirectoryName
                                            | System.IO.NotifyFilters.FileName
                                            | System.IO.NotifyFilters.LastAccess
                                            | System.IO.NotifyFilters.LastWrite
                                            | System.IO.NotifyFilters.Security
                                            | System.IO.NotifyFilters.Size;
            FileSystemWatcher.Changed += OnChanged;
            FileSystemWatcher.Filter = System.IO.Path.GetFileName(LogPath);
            FileSystemWatcher.EnableRaisingEvents = true;
            FileSystemWatcher.IncludeSubdirectories = true;

            ReadFile();
        }

        private void OnChanged(object sender, System.IO.FileSystemEventArgs e)
        {
            ReadFile();
        }

        private void ReadFile()
        {
            ReadCounter++;

            using (var fileStream = new System.IO.FileStream(LogPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite))
            {
                using (var reader = new System.IO.StreamReader(fileStream))
                {
                    LogData = reader.ReadToEnd();
                }
            }
        }

        public void Update()
        {
            uint roundedTime = (uint)System.Math.Round(Config.TotalDelta);
            bool read = roundedTime % SecondsPerRead == 0;
            TimeSinceLastLogRead = Config.TotalDelta - LastLogRead;

            if (read == true && TimeSinceLastLogRead > SecondsPerRead)
            {
                LastLogRead = roundedTime;

                ReadFile();
            }
        }
    }
}
