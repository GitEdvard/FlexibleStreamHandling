using System.IO;

namespace FlexibleStreamHandling
{
    public class FileIOStream : FlexibleStream
    {
        private readonly string _filePath;
        private readonly FileMode _fileMode;
        private readonly FileAccess _fileAccess;
        private FileStream _fileStream;
        public bool DeleteWhenReady { get; set; }

        public FileIOStream(string filePath, 
            FileMode fileMode = FileMode.Open, 
            FileAccess fileAccess = FileAccess.Read)
        {
            _filePath = filePath;
            _fileMode = fileMode;
            _fileAccess = fileAccess;
        }

        public override string GetFileName()
        {
            return Path.GetFileName(_filePath);
        }

        protected override void Dispose(bool disposing)
        {
            if (Disposed)
                return;
            base.Dispose(disposing);

            if (DeleteWhenReady)
            {
                File.Delete(_filePath);
            }
        }

        protected override Stream Stream => _fileStream 
            ?? (_fileStream = new FileStream(_filePath, _fileMode, _fileAccess));
        protected override void CloseStream()
        {
            _fileStream?.Close();
        }
    }
}
