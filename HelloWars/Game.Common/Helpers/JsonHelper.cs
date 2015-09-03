using System.IO;
using Newtonsoft.Json;

namespace Common.Helpers
{
    public static class JsonHelper
    {
        public static T Deserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
