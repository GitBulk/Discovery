using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace ChatRoom.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>Serializes the object to a JSON string.</summary>
        /// <returns>A JSON string representation of the object.</returns>
        public static string ToCamelCaseJson(this object value)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            };

            return JsonConvert.SerializeObject(value, settings);
        }
    }
}