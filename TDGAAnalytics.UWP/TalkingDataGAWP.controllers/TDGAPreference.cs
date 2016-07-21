using System;
using System.IO.IsolatedStorage;
using TalkingDataGAWP.command;

namespace TalkingDataGAWP.controllers
{
	internal class TDGAPreference
	{
		public const long DEFAULT_SESSION_END_TIME = 0L;

		public const long DEFAULT_INIT_TIME = 0L;

		public const long DEFAULT_SESSION_START_TIME = 868L;

		private static string SESSION_START_TIME_LONG = "TDGApref.session.start.key";

		private static string SESSION_START_CURRENT_TIME_MILLIS = "TDGApref.session.startsystem.key";

		private static string SESSION_END_TIME_LONG = "TDGApref.session.end.key";

		private static string ACTIVE_SESSION_ID_KEY = "TDGApref.activesessionid.key";

		private static string MESSION_ID = "TDGApref.missionid.key";

		private static string APPLIST_SEND_TIME = "pref.applisttime.key";

		private static string ACCOUNT_ID_KEY = "pref.accountid.key";

		private static string ACCOUNT_GAME_KEY = "pref.accountgame.key";

		private static string PREF_INIT_KEY = "pref.init.key";

		private static string DEVICE_ID_KEY = "pref.deviceid.key";

		public static void init()
		{
		}

		public static string getActiveSessionId()
		{
			string empty = string.Empty;
			if (IsolatedStorageSettings.get_ApplicationSettings().Contains(TDGAPreference.ACTIVE_SESSION_ID_KEY))
			{
				IsolatedStorageSettings.get_ApplicationSettings().TryGetValue<string>(TDGAPreference.ACTIVE_SESSION_ID_KEY, ref empty);
			}
			return empty;
		}

		public static void setActiveSessionID(string sid)
		{
			TDGAPreference.setKeyValue(TDGAPreference.ACTIVE_SESSION_ID_KEY, sid);
		}

		public static long getInitTime()
		{
			long result = 0L;
			if (IsolatedStorageSettings.get_ApplicationSettings().Contains(TDGAPreference.PREF_INIT_KEY))
			{
				IsolatedStorageSettings.get_ApplicationSettings().TryGetValue<long>(TDGAPreference.PREF_INIT_KEY, ref result);
			}
			return result;
		}

		public static void setInitTime(long t)
		{
			TDGAPreference.setKeyValue(TDGAPreference.PREF_INIT_KEY, t);
		}

		public static string getAccountGameServer(string accountid)
		{
			string empty = string.Empty;
			if (IsolatedStorageSettings.get_ApplicationSettings().Contains(accountid + TDGAPreference.ACCOUNT_GAME_KEY))
			{
				IsolatedStorageSettings.get_ApplicationSettings().TryGetValue<string>(accountid + TDGAPreference.ACCOUNT_GAME_KEY, ref empty);
			}
			return empty;
		}

		public static void setAccountGameServer(string accountId, string gameserver)
		{
			TDGAPreference.setKeyValue(accountId + TDGAPreference.ACCOUNT_GAME_KEY, gameserver);
		}

		public static string getAccountID()
		{
			string empty = string.Empty;
			if (IsolatedStorageSettings.get_ApplicationSettings().Contains(TDGAPreference.ACCOUNT_ID_KEY))
			{
				IsolatedStorageSettings.get_ApplicationSettings().TryGetValue<string>(TDGAPreference.ACCOUNT_ID_KEY, ref empty);
			}
			return empty;
		}

		public static void setAccountID(string accountId)
		{
			TDGAPreference.setKeyValue(TDGAPreference.ACCOUNT_ID_KEY, accountId);
		}

		public static string getMissionID()
		{
			string empty = string.Empty;
			if (IsolatedStorageSettings.get_ApplicationSettings().Contains(TDGAPreference.MESSION_ID))
			{
				IsolatedStorageSettings.get_ApplicationSettings().TryGetValue<string>(TDGAPreference.MESSION_ID, ref empty);
			}
			return empty;
		}

		public static void setMissionID(string missonId)
		{
			TDGAPreference.setKeyValue(TDGAPreference.MESSION_ID, missonId);
		}

		public static long getActiveSessionStartTime()
		{
			long result = 868L;
			if (IsolatedStorageSettings.get_ApplicationSettings().Contains(TDGAPreference.SESSION_START_TIME_LONG))
			{
				IsolatedStorageSettings.get_ApplicationSettings().TryGetValue<long>(TDGAPreference.SESSION_START_TIME_LONG, ref result);
			}
			return result;
		}

		public static void setActiveSessionStartTime(long start)
		{
			TDGAPreference.setKeyValue(TDGAPreference.SESSION_START_TIME_LONG, start);
		}

		public static long getSessionStartCurrentTimeMillis()
		{
			long result = 868L;
			if (IsolatedStorageSettings.get_ApplicationSettings().Contains(TDGAPreference.SESSION_START_CURRENT_TIME_MILLIS))
			{
				IsolatedStorageSettings.get_ApplicationSettings().TryGetValue<long>(TDGAPreference.SESSION_START_CURRENT_TIME_MILLIS, ref result);
			}
			return result;
		}

		public static void setSessionStartCurrentTimeMillis()
		{
			TDGAPreference.setKeyValue(TDGAPreference.SESSION_START_CURRENT_TIME_MILLIS, DateTimeUtils.getCurrentTime());
		}

		public static long getActiveSessionEndTime()
		{
			long result = 0L;
			if (IsolatedStorageSettings.get_ApplicationSettings() == null)
			{
				TDGAPreference.init();
			}
			if (IsolatedStorageSettings.get_ApplicationSettings().Contains(TDGAPreference.SESSION_END_TIME_LONG))
			{
				IsolatedStorageSettings.get_ApplicationSettings().TryGetValue<long>(TDGAPreference.SESSION_END_TIME_LONG, ref result);
			}
			return result;
		}

		public static void setActiveSessionEndTime(long end)
		{
			TDGAPreference.setKeyValue(TDGAPreference.SESSION_END_TIME_LONG, end);
		}

		public static string getDeviceID()
		{
			string empty = string.Empty;
			if (IsolatedStorageSettings.get_ApplicationSettings().Contains(TDGAPreference.DEVICE_ID_KEY))
			{
				IsolatedStorageSettings.get_ApplicationSettings().TryGetValue<string>(TDGAPreference.DEVICE_ID_KEY, ref empty);
			}
			return empty;
		}

		public static void setDeviceID(string deviceid)
		{
			TDGAPreference.setKeyValue(TDGAPreference.DEVICE_ID_KEY, deviceid);
		}

		private static void setKeyValue(string key, object value)
		{
			if (IsolatedStorageSettings.get_ApplicationSettings().Contains(key))
			{
				IsolatedStorageSettings.get_ApplicationSettings().set_Item(key, value);
			}
			else
			{
				IsolatedStorageSettings.get_ApplicationSettings().Add(key, value);
			}
			IsolatedStorageSettings.get_ApplicationSettings().Save();
		}
	}
}
