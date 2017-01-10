using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;
using Tam.Queue.Logging;

namespace Tam.Queue.Serialization
{
    public class JsonSerializationStrategy : ISerializationStrategy
    {
        private readonly ISerializer serializer;

        public JsonSerializationStrategy(ISerializer serializer)
        {
            this.serializer = serializer;
        }

        public string ContentEncoding
        {
            get
            {
                return "utf-8";
            }
        }

        public string ContentType
        {
            get
            {
                return "application/json; charset=utf-8";
            }
        }

        public T Deserialize<T>(byte[] bytes)
        {
            try
            {
                string message = Encoding.UTF8.GetString(bytes);
                return this.serializer.Deserialize<T>(message);
            }
            catch (Exception ex)
            {
                string error = "An error occurred attempting to serialize the provided message: " + ex.Message;
                Logger.Current.Write(new LogEntry
                {
                    Message = error,
                    Severity = TraceEventType.Error
                });
                throw new SerializationException(error, ex);
            }
        }

        public byte[] Serialize<T>(T message)
        {
            try
            {
                string json = this.serializer.Serialize<T>(message);
                return Encoding.UTF8.GetBytes(json);
            }
            catch (Exception ex)
            {
                string error = "An error occurred attempting to serialize the provided message: " + ex.Message;
                Logger.Current.Write(new LogEntry
                {
                    Message = error,
                    Severity = TraceEventType.Error
                });
                throw new SerializationException(error, ex);
            }
        }
    }
}
