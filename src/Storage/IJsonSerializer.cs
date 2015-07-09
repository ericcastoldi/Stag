
namespace Stag.Storage
{
    internal interface IJsonSerializer
    {
        string Serialize(object target);

        T Deserialize<T>(string target);
    }
}
