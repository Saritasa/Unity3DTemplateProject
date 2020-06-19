using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;

namespace API.ContractResolvers
{
    /// <summary>
    /// This resolver allows to omit those fields which can't be serialized to JSON with null values
    /// In fact it replaces JsonProperty attribute parameter Required.DisallowNull to NullValueHandling.Ignore
    /// 
    /// Use case:
    /// class T
    /// {
    ///     [JsonProperty(Required = Required.DisallowNull)]
    ///     public string s;
    /// }
    /// 
    /// JsonConvert.SerializeObject<T>(new T()) // Raise exception
    /// var ret = JsonConvert.SerializeObject<T>(
    ///     new T(),
    ///     new JsonSerializerSettings() { ContractResolver = OmitNotNullableFieldIfNull.Instance});
    /// ret == "{ }"; // true
    /// </summary>
    public class OmitNotNullableFieldIfNull : DefaultContractResolver
    {
        public new static readonly OmitNotNullableFieldIfNull Instance = new OmitNotNullableFieldIfNull();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            if (property.Required == Required.DisallowNull)
            {
                property.Required = Required.Default;
                property.NullValueHandling = NullValueHandling.Ignore;
            }
            return property;
        }

        public static JsonSerializerSettings GetSettings()
        {
            return new JsonSerializerSettings()
            {
                ContractResolver = OmitNotNullableFieldIfNull.Instance
            };
        }
    }
}