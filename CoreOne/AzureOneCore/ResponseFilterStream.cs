using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AzureOneCore.Filter
{
    public class ResponseFilterStream : Stream
    {
        private readonly Stream responseStream;
        StringBuilder builder = new StringBuilder();

        public override bool CanRead
        {
            get
            {
                return false;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        public override long Length
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public override long Position
        {
            get
            {
                throw new NotSupportedException();
            }

            set
            {
                throw new NotSupportedException();
            }
        }

        public ResponseFilterStream(Stream responseStream)
        {
            if (responseStream == null)
            {
                throw new ArgumentNullException("responseStream");
            }
            this.responseStream = responseStream;
        }

        public override void Flush()
        {
            this.responseStream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            string html = Encoding.UTF8.GetString(buffer, offset, count);
            var reg = new Regex(@"(?<=\s)\s+(?![^<>]*</pre>)");
            html = reg.Replace(html, string.Empty);
            buffer = Encoding.UTF8.GetBytes(html);
            this.responseStream.Write(buffer, 0, buffer.Length);
        }
    }
}