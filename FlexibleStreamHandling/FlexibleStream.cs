using System;
using System.IO;
using System.Text;

namespace FlexibleStreamHandling
{
    public abstract class FlexibleStream : IDisposable
    {
        protected abstract Stream WriteStream { get; }

        public abstract Stream ReadStream { get; }

        public abstract Encoding Encoding { get; }

        protected StreamWriter StreamWriter;
        protected bool Disposed;
        /// <summary>
        /// Wraps MemoryStream and FileStream classes to provide a StreamReader
        /// </summary>
        /// <param name="stream"></param>
        public FlexibleStream()
        {
        }

        public StreamReader GetReader()
        {
            Flush();
            ReadStream.Position = 0;
            return new StreamReader(ReadStream);
        }

        private void Flush()
        {
            StreamWriter?.Flush();
            ReadStream?.Flush();
        }
        [Obsolete]
        public Stream GetStream()
        {
            Flush();
            WriteStream.Position = 0;
            return WriteStream;
        }

        public Stream GetWriteStream()
        {
            Flush();
            WriteStream.Position = 0;
            return WriteStream;
        }

        public Stream GetReadStream()
        {
            Flush();
            ReadStream.Position = 0;
            return ReadStream;
        }

        public abstract string GetFileName();

        public long SizeMB
        {
            get { return ReadStream.Length/1000000; }
        }

        private void InitWriter()
        {
            if (StreamWriter == null)
            {
                StreamWriter = new StreamWriter(WriteStream, Encoding);
            }            
        }

        public void WriteLine(string text)
        {
            InitWriter();
            StreamWriter.WriteLine(text);
        }

        public void Write(string text)
        {
            InitWriter();
            StreamWriter.Write(text);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            Flush();

            WriteStream?.Close();
            ReadStream?.Close();
            Disposed = true;
        }
    }
}
