using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace WpfUtil.Data
{
    public static class Json
    {
        public static string Serialize<T>(T obj)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));

            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);

                string json = Encoding.UTF8.GetString(ms.ToArray());

                return json;
            }
        }

        public static T DeSerialize<T>(string json)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                T obj = (T)serializer.ReadObject(ms);

                return obj;
            }
        }
    }
}
