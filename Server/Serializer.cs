using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T output)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(output));
        }

        public T Deserialize<T>(byte[] source)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(source));
        }
    }
}
