using System.IO;

namespace FlexibleStreamHandling
{
    public class FileIOStream : FlexibleStream
    {
        private readonly string _filePath;
        private FileMode _fileMode;
        private FileAccess _fileAccess;
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
            return System.IO.Path.GetFileName(_filePath);
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

        public override Stream Stream => _fileStream 
            ?? (_fileStream = new FileStream(_filePath, _fileMode, _fileAccess));

        public override string Path => _filePath;
        protected override void CloseStream()
        {
            _fileStream?.Close();
        }

        public override void ReOpenAs(FileMode fileMode, FileAccess fileAccess)
        {
            _fileMode = fileMode;
            _fileAccess = fileAccess;
            Dispose();
            _fileStream = null;
            Disposed = false;

        }
    }
}
