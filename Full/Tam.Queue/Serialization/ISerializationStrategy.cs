using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tam.Queue.Serialization
{
    public interface ISerializationStrategy
    {
        string ContentType { get; }
        string ContentEncoding { get; }
        byte[] Serialize<T>(T message);
        T Deserialize<T>(byte[] bytes);
    }
}
