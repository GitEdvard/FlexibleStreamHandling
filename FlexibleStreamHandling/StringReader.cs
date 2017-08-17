using System.IO;

namespace FlexibleStreamHandling
{
    /// <summary>
    /// Used for input to classes that may fetch text from either a file or a string
    /// </summary>
    public class StringReader : FlexibleStream
    {
        private readonly string _fileName;
        private MemoryStream _memoryStream;

        public StringReader() : this("") {}

        public StringReader(string contents, string fileName = "test")
        {
            _fileName = fileName;
            Write(contents);
        }

        public override string GetFileName()
        {
            return _fileName;
        }

        public override Stream Stream => _memoryStream ?? (_memoryStream = new MemoryStream());
        public override string Path => $@"c:\{_fileName}";
        protected override void CloseStream()
        {
            Stream.Close();
        }

        public override void ReOpenAs(FileMode fileMode, FileAccess fileAccess)
        {
            Dispose();
            _memoryStream = null;
            Disposed = false;
        }
    }
}
