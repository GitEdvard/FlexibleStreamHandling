using System.IO;
using System.Text;

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

        protected override Stream WriteStream => _memoryStream ?? (_memoryStream = new MemoryStream());
        public override Stream ReadStream => _memoryStream ?? (_memoryStream = new MemoryStream());
        public override Encoding Encoding => Encoding.UTF8;
    }
}
