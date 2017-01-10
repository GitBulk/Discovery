using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Tam.Queue.Serialization
{
    public class BinarySerializationStrategy : ISerializationStrategy
    {
        public string ContentEncoding
        {
            get
            {
                return string.Empty;
            }
        }

        public string ContentType
        {
            get
            {
                return "application/x-dotnet-serialized-object";
            }
        }

        public byte[] Serialize<T>(T message)
        {
            using (var stream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, message);
                return stream.GetBuffer();
            }
        }

        public T Deserialize<T>(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                IFormatter formatter = new BinaryFormatter();
                object o = formatter.Deserialize(stream);
                return (T)o;
            }
        }
    }
}
