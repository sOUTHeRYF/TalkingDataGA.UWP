using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using TalkingDataGAWP.command;
using TalkingDataGAWP.Entity;

namespace TalkingDataGAWP.controllers
{
	internal class TDGAEventListManager
	{
		private static List<string> g_EventList = null;

		internal static string glFileName = "tdga_talkingdata_file";

		public static void init()
		{
			if (TDGAEventListManager.g_EventList == null)
			{
				TDGAEventListManager.g_EventList = TDGAEventListManager.loadEventList();
			}
		}

		public static void saveEventList()
		{
			Monitor.Enter(TDGAEventListManager.g_EventList);
			if (TDGAEventListManager.g_EventList != null)
			{
				IsolatedStorageUtils.Save<List<string>>("TalkingDataGA", TDGAEventListManager.glFileName, TDGAEventListManager.g_EventList);
			}
			TDGAEventListManager.g_EventList.Clear();
			Monitor.Exit(TDGAEventListManager.g_EventList);
		}

		[MethodImpl(32)]
		public static void addEvent(EventBase mevent, bool sendRightNow)
		{
			TDGAEventListManager.init();
			string text = mevent.toJsonString();
			Debugger.Log(text);
			TDGAEventListManager.g_EventList.Add(text);
			if (sendRightNow)
			{
				TDGAHttpManager.getInstance().sendDataRightNow();
			}
		}

		public static void addEvent(EventBase mevent)
		{
			TDGAEventListManager.init();
			string text = mevent.toJsonString();
			Debugger.Log(text);
			TDGAEventListManager.g_EventList.Add(text);
		}

		public static void sendEventSuccess()
		{
			IsolatedStorageUtils.Delete("TalkingDataGA", TDGAEventListManager.glFileName);
		}

		public static void sendEventFaild()
		{
			TDGAEventListManager.init();
			TDGAEventListManager.g_EventList.AddRange(TDGAEventListManager.loadEventList());
		}

		public static List<string> loadEventList()
		{
			List<string> result;
			if (IsolatedStorageUtils.fileExists("TalkingDataGA\\" + TDGAEventListManager.glFileName))
			{
				result = IsolatedStorageUtils.Get<List<string>>("TalkingDataGA", TDGAEventListManager.glFileName);
			}
			else
			{
				result = new List<string>();
			}
			return result;
		}

		public static int eventCount()
		{
			TDGAEventListManager.init();
			return TDGAEventListManager.g_EventList.get_Count();
		}
	}
}
