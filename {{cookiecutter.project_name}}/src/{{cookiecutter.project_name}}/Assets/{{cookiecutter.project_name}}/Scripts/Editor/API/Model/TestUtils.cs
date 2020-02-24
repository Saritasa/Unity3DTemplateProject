using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using API.ContractResolvers;

namespace API.Models.Test
{
    public static partial class TestUtils
    {
        private static T ConvertTo<T>(object value)
        {
            T returnValue;
            try
            {
                //Handling Nullable types i.e, int?, double?, bool? .. etc
                if (Nullable.GetUnderlyingType(typeof(T)) != null)
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                    returnValue = (T)conv.ConvertFrom(value);
                }
                else
                {
                    returnValue = (T)Convert.ChangeType(value, typeof(T));
                }
            }
            catch (Exception)
            {
#if UNITY_EDITOR
                UnityEngine.Debug.LogErrorFormat("Unable to convert {0} to {1}", value.GetType().ToString(), typeof(T).ToString());
#endif
                returnValue = default(T);
            }
            return returnValue;
        }

        public static bool TestSerialization<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented, OmitNotNullableFieldIfNull.GetSettings());
            var deserializedObj = JsonConvert.DeserializeObject<T>(json);

            var json2 = JsonConvert.SerializeObject(obj, Formatting.Indented, OmitNotNullableFieldIfNull.GetSettings());

            var jsonMatch = json == json2;

            if (!jsonMatch)
            {
                var ret = new StringBuilder();
                ret.AppendFormat("JSON test mistmatch for type: {0}", typeof(T).ToString());
                ret.AppendLine("JSON1:");
                ret.AppendLine(json);
                ret.AppendLine("JSON2:");
                ret.AppendLine(json2);
                UnityEngine.Debug.LogWarningFormat(ret.ToString());
            }
            return jsonMatch;
        }

        public static void SetExampleData<T>(ref T obj, string value)
        {
            obj = ConvertTo<T>(value);
        }

        public static void SetExampleData<T>(ref List<T> obj, string value)
        {
            obj = new List<T>();
            try
            {
                var ret = value.Trim('[', ']');
                var items = ret.Split(',');
                foreach(var item in items)
                {
                    obj.Add(ConvertTo<T>(item.Trim()));
                }
            }
            catch
            {
                UnityEngine.Debug.LogFormat("Unable to parse string to list: {0}", value);
                SetExampleData(ref obj);
            }
        }

        public static void SetExampleData<T>(ref T obj)
        {
            obj = default(T);
        }

        public static void SetExampleData<T>(ref List<T> obj)
        {
            obj = new List<T>() { default(T), default(T) };
        }
    }
}
