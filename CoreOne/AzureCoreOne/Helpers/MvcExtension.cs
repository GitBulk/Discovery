using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace AzureCoreOne.Helpers
{
    public static class MvcExtension
    {
        public static void AddMvcCamelCasePropertyNames(this IMvcBuilder builder)
        {
            builder.AddJsonOptions(o => o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
        }
    }
}
