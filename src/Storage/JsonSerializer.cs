using Newtonsoft.Json;

namespace Stag.Storage
{
    internal class JsonSerializer : IJsonSerializer
    {
        public string Serialize(object target)
        {
            return JsonConvert.SerializeObject(target);
        }

        public T Deserialize<T>(string target)
        {
            return JsonConvert.DeserializeObject<T>(target);
        }
    }
}
