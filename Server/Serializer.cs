using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Serializer
    {
        public static byte[] Serialize(Output output)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(output));
        }

        public static Input Deserialize(byte[] source)
        {
            return JsonConvert.DeserializeObject<Input>(Encoding.UTF8.GetString(source));
        }
    }
}
