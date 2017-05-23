using System.IO;

namespace FlexibleStreamHandling
{
    /// <summary>
    /// Used for input to classes that may fetch text from either a file or a string
    /// </summary>
    public class StringReader : FlexibleStream
    {
        private readonly string _fileName;

        public StringReader() : this("") {}

        public StringReader(string contents, string fileName = "test")
            : base(new MemoryStream())
        {
            _fileName = fileName;
            Write(contents);
        }

        public override string GetFileName()
        {
            return _fileName;
        }
    }
}
