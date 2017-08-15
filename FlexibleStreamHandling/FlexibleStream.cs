using System;
using System.IO;
using System.Text;

namespace FlexibleStreamHandling
{
    public abstract class FlexibleStream : IDisposable
    {
        protected StreamWriter StreamWriter;
        protected bool Disposed;
        /// <summary>
        /// Wraps MemoryStream and FileStream classes to provide a StreamReader
        /// </summary>
        /// <param name="stream"></param>
        public FlexibleStream()
        {
        }

        public abstract Stream Stream { get; }

        public abstract string Path { get; }
        public StreamReader GetReader()
        {
            Flush();
            Stream.Position = 0;
            return new StreamReader(Stream);
        }

        private void Flush()
        {
            if (StreamWriter != null)
            {
                StreamWriter.Flush();
            }
        }

        public Stream GetStream()
        {
            return Stream;
        }

        public abstract string GetFileName();

        public long SizeMB
        {
            get { return Stream.Length/1000000; }
        }

        private void InitWriter()
        {
            if (StreamWriter == null)
            {
                StreamWriter = new StreamWriter(Stream, Encoding.GetEncoding(1252));
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

        protected abstract void CloseStream();

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            Flush();
            CloseStream();

            Disposed = true;
        }
    }
}
