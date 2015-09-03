using System.IO;
using Newtonsoft.Json;

namespace Arena.Helpers
{
    public static class JsonHelper<T>
    {
        public static T Deserialize(string jsonString)
        {
            var jsonSerializer = JsonSerializer.Create();
            var reader = new JsonTextReader(new StringReader(jsonString));
            return jsonSerializer.Deserialize<T>(reader);
        }
    }
}
