using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;

using Windows.Storage;
namespace TalkingDataGAWP.command
{
	internal class PersistedSettingUtils
	{
		private static readonly ApplicationDataContainer _isolatedStore = ApplicationData.Current.LocalSettings;

     //   private static readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

		public static void AddOrUpdateValue(string key, object value)
		{
            if (CheckInput(key))
            {
                _isolatedStore.Values[GetUnrepeatedKey(key)] = value;
            }
		}

		public static T GetValueOrDefault<T>(string key, T defaultValue)
		{
            T result = defaultValue;
            if (CheckInput(key))
            {
                try
                {
                    if (_isolatedStore.Values.ContainsKey(GetUnrepeatedKey(key)))
                    {
                        result = (T)_isolatedStore.Values[GetUnrepeatedKey(key)];
                    }
                }
                catch (Exception)
                {
                    result = defaultValue;
                }
            }
            return result;
		}
        private  static bool CheckInput(string key)
        {
            return !string.IsNullOrWhiteSpace(key);
        }
        private static string GetUnconflictedKey(string key)
        {
            return "TDGA_Y" + key;
        }
	}
}
