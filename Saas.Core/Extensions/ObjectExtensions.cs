using Newtonsoft.Json;

namespace Saas.Core.Extensions
{
    //TODO: Herhangi bir objeyi json stringine çevirir.
    public static class ObjectExtensions
    {
        public static string ToJson(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }


}
