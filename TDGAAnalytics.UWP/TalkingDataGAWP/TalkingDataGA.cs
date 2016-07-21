using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Windows;
using TalkingDataGAWP.command;
using TalkingDataGAWP.controllers;
using TalkingDataGAWP.Entity;

namespace TalkingDataGAWP
{
	public class TalkingDataGA
	{
		internal static bool isAlreadyInit;

		private static string sAppId;

		private static string sPartnerId;

		public static string getAppKey()
		{
			return TalkingDataGA.sAppId;
		}

		public static string getPartnerId()
		{
			return TalkingDataGA.sPartnerId;
		}

		public static string getDeviceID()
		{
			return TDGAPreference.getDeviceID();
		}

		public static void init(string appkey, string channel)
		{
			if (TalkingDataGA.isAlreadyInit)
			{
				return;
			}
			Debugger.Log(string.Concat(new string[]
			{
				"Init SDK Version:",
				Constants.SDK_VERSION,
				"   APPID:",
				appkey,
				"  Channel:",
				channel
			}));
			VOAppProfile.getInstance().partner = TalkingDataGA.validChannel(channel);
			VOAppProfile.getInstance().sequenceNumber = appkey;
			VODeviceProfile.getInstance();
			PhoneApplicationService.get_Current().add_Deactivated(new EventHandler<DeactivatedEventArgs>(TalkingDataGA.Current_Deactivated));
			PhoneApplicationService.get_Current().add_Activated(new EventHandler<ActivatedEventArgs>(TalkingDataGA.Current_Activated));
			PhoneApplicationService.get_Current().add_Closing(new EventHandler<ClosingEventArgs>(TalkingDataGA.Current_Closing));
			Debugger.Log("finish add handler");
			if (TalkingDataGA.isAppkValided(appkey))
			{
				TalkingDataGA.sAppId = appkey;
				TalkingDataGA.sPartnerId = channel;
				long currentTime = DateTimeUtils.getCurrentTime();
				TDGAPreference.init();
				if (TDGAPreference.getInitTime() == 0L)
				{
					TDGAEventListManager.addEvent(new EventInit());
					TDGAPreference.setInitTime(currentTime);
				}
				TDGAAccount.sAccount = TDGAAccount.getTDGAccount();
				TalkingDataGA.isAlreadyInit = true;
				TalkingDataGA.startSession();
				return;
			}
			Debugger.Log("Your appKey is invalid ,please apply for a new one");
		}

		public static void onEvent(string eventid, Dictionary<string, object> map)
		{
			if (eventid != null && !eventid.Trim().Equals(string.Empty))
			{
				TDGAEventListManager.addEvent(new EventCustomEvent(TDGASessionController.sGameSessionId, TDGAAccount.sAccount, eventid, map));
			}
		}

		public static void onEvent(string eventid)
		{
			TalkingDataGA.onEvent(eventid, null);
		}

		private static void Current_Closing(object sender, ClosingEventArgs e)
		{
			if (PhoneApplicationService.get_Current() != null)
			{
				PhoneApplicationService.get_Current().remove_Deactivated(new EventHandler<DeactivatedEventArgs>(TalkingDataGA.Current_Deactivated));
				PhoneApplicationService.get_Current().remove_Activated(new EventHandler<ActivatedEventArgs>(TalkingDataGA.Current_Activated));
				PhoneApplicationService.get_Current().remove_Closing(new EventHandler<ClosingEventArgs>(TalkingDataGA.Current_Closing));
			}
			TalkingDataGA.pauseSession();
		}

		private static void Current_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
		{
			TalkingDataGA.pauseSession();
		}

		private static void Current_Activated(object sender, ActivatedEventArgs e)
		{
			TalkingDataGA.startSession();
		}

		private static void Current_Deactivated(object sender, DeactivatedEventArgs e)
		{
			TalkingDataGA.pauseSession();
		}

		private static string validChannel(string channel)
		{
			if (string.IsNullOrEmpty(channel.Trim()))
			{
				channel = "TD_unknown";
			}
			return channel;
		}

		private static bool isAppkValided(string appkey)
		{
			return !string.IsNullOrWhiteSpace(appkey) && appkey.Length == 32;
		}

		private static void startSession()
		{
			TDGASessionController.sessionResume();
			TDGAHttpManager.getInstance().sendDataWithDetalTime();
		}

		private static void pauseSession()
		{
			TDGASessionController.pauseSession();
			TDGAHttpManager.getInstance().stopSendDataWithDetalTime();
			TDGAEventListManager.saveEventList();
		}
	}
}
