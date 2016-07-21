using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;

namespace TalkingDataGAWP.command
{
	internal class PersistedSettingUtils
	{
		private static readonly IsolatedStorageSettings _isolatedStore = IsolatedStorageSettings.get_ApplicationSettings();

		private static readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

		public static void AddOrUpdateValue(string key, object value)
		{
			bool flag = false;
			try
			{
				if (PersistedSettingUtils._isolatedStore.Contains(key))
				{
					if (PersistedSettingUtils._isolatedStore.get_Item(key) != value)
					{
						PersistedSettingUtils._isolatedStore.set_Item(key, value);
						flag = true;
					}
				}
				else
				{
					PersistedSettingUtils._isolatedStore.Add(key, value);
					flag = true;
				}
			}
			catch (KeyNotFoundException)
			{
				PersistedSettingUtils._isolatedStore.Add(key, value);
				flag = true;
			}
			catch (ArgumentException)
			{
				PersistedSettingUtils._isolatedStore.Add(key, value);
				flag = true;
			}
			if (flag)
			{
				PersistedSettingUtils.Save();
				PersistedSettingUtils.UpdateCache(key, value);
			}
		}

		public static T GetValueOrDefault<T>(string key, T defaultValue)
		{
			object obj = null;
			if (!PersistedSettingUtils._cache.TryGetValue(key, ref obj))
			{
				T t;
				try
				{
					if (PersistedSettingUtils._isolatedStore.Contains(key))
					{
						t = (T)((object)PersistedSettingUtils._isolatedStore.get_Item(key));
					}
					else
					{
						t = defaultValue;
					}
				}
				catch (KeyNotFoundException)
				{
					t = defaultValue;
				}
				catch (ArgumentException)
				{
					t = defaultValue;
				}
				PersistedSettingUtils.UpdateCache(key, t);
				return t;
			}
			return (T)((object)obj);
		}

		private static void UpdateCache(string key, object value)
		{
			Dictionary<string, object> cache = PersistedSettingUtils._cache;
			lock (cache)
			{
				if (PersistedSettingUtils._cache.ContainsKey(key))
				{
					PersistedSettingUtils._cache.set_Item(key, value);
				}
				else
				{
					PersistedSettingUtils._cache.Add(key, value);
				}
			}
		}

		private static void Save()
		{
			PersistedSettingUtils._isolatedStore.Save();
		}
	}
}
