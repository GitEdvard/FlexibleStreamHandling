using System.IO;

namespace FlexibleStreamHandling
{
    public class FileIOStream : FlexibleStream
    {
        private string _filePath;
        public bool DeleteWhenReady { get; set; }

        public FileIOStream(string filePath, 
            FileMode fileMode = FileMode.Open, 
            FileAccess fileAccess = FileAccess.Read):
            base(new FileStream(filePath, fileMode, fileAccess))
        {
            _filePath = filePath;
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
    }
}
