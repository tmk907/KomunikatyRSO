using Newtonsoft.Json;
using System;
using System.Reflection;
using Windows.Storage;

namespace KomunikatyRSO.UWP.Shared.Settings
{
    public class SettingsHelper
    {
        public SettingsHelper()
        {
            Settings = ApplicationData.Current.LocalSettings;
        }

        private ApplicationDataContainer Settings { get; set; }

        private JsonSerializer serializer = new JsonSerializer();

        public T Read<T>(string key, T @default = default(T))
        {
            object value = null;

            if (!Settings.Values.TryGetValue(key, out value))
            {
                return @default;
            }

            if (value == null)
            {
                return @default;
            }

            var type = typeof(T);
            var typeInfo = type.GetTypeInfo();

            if (typeInfo.IsPrimitive || type == typeof(string))
            {
                return (T)Convert.ChangeType(value, type);
            }

            return JsonConvert.DeserializeObject<T>((string)value);
        }

        public void Save<T>(string key, T value)
        {
            var type = typeof(T);
            var typeInfo = type.GetTypeInfo();

            if (typeInfo.IsPrimitive || type == typeof(string))
            {
                Settings.Values[key] = value;
            }
            else
            {
                Settings.Values[key] = JsonConvert.SerializeObject(value);
            }
        }
    }
}
