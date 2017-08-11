using System.IO;
using System.Text;

namespace FlexibleStreamHandling
{
    public class FileIOStream : FlexibleStream
    {
        private readonly string _filePath;
        private readonly FileMode _fileModeForWrite;
        private FileStream _writeStream;
        private FileStream _readStream;
        private Encoding _encoding;
        public bool DeleteWhenReady { get; set; }

        public FileIOStream(string filePath,
            Encoding encodingForWrite = null,
            FileMode fileModeForWrite = FileMode.Create)
        {
            _filePath = filePath;
            _fileModeForWrite = fileModeForWrite;
            _encoding = encodingForWrite;
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

        protected override Stream WriteStream => _writeStream 
            ?? (_writeStream = new FileStream(_filePath, _fileModeForWrite, FileAccess.Write));

        public override Stream ReadStream => _readStream 
            ?? (_readStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read));

        public override Encoding Encoding => _encoding ?? (_encoding = Encoding.UTF8);
    }
}
