using System;
using System.Runtime.CompilerServices;
using System.Text;
using TalkingDataGAWP.command;
using TalkingDataGAWP.Entity;

namespace TalkingDataGAWP.controllers
{
	internal class TDGASessionController
	{
		public static string sGameSessionId = "";

		private const long NEW_SESSION_INTERVAL = 30000L;

		[MethodImpl(32)]
		public static void sessionResume()
		{
			long activeSessionStartTime = TDGAPreference.getActiveSessionStartTime();
			long activeSessionEndTime = TDGAPreference.getActiveSessionEndTime();
			string activeSessionId = TDGAPreference.getActiveSessionId();
			int interval = 0;
			long currentTime = DateTimeUtils.getCurrentTime();
			bool flag = false;
			TDGASessionController.sGameSessionId = activeSessionId;
			if (activeSessionStartTime == 868L)
			{
				flag = true;
				interval = 0;
			}
			else if ((currentTime - activeSessionEndTime > 30000L && currentTime - activeSessionStartTime > 30000L) || currentTime < activeSessionStartTime)
			{
				flag = true;
				if (activeSessionEndTime == 0L)
				{
					string arg_5A_0 = activeSessionId;
					long expr_59 = activeSessionStartTime;
					TDGASessionController.saveSessionEnd(arg_5A_0, expr_59, expr_59);
					interval = 0;
				}
				else
				{
					TDGASessionController.saveSessionEnd(activeSessionId, activeSessionStartTime, activeSessionEndTime);
					interval = (int)(currentTime - activeSessionEndTime) / 1000;
				}
			}
			if (flag)
			{
				TDGASessionController.generateGameSessionId(currentTime, interval, TDGAAccount.sAccount);
			}
		}

		public static void pauseSession()
		{
			TDGAPreference.setActiveSessionEndTime(DateTimeUtils.getCurrentTime());
			if (TDGAAccount.sAccount != null)
			{
				TDGAAccount.sAccount.updateGameDuration();
			}
		}

		internal static void changeAccount(TDGAAccount old, TDGAAccount current)
		{
			TDGASessionController.restartSession(old, current);
		}

		[MethodImpl(32)]
		public static void restartSession(TDGAAccount preAccount, TDGAAccount nextAccount)
		{
			long activeSessionStartTime = TDGAPreference.getActiveSessionStartTime();
			string arg_1A_0 = TDGAPreference.getActiveSessionId();
			long currentTime = DateTimeUtils.getCurrentTime();
			TDGAEventListManager.addEvent(new EventLogout(arg_1A_0, preAccount, TDGAPreference.getSessionStartCurrentTimeMillis(), currentTime - activeSessionStartTime));
			TDGASessionController.generateGameSessionId(currentTime, 0, nextAccount);
		}

		private static void generateGameSessionId(long time, int interval, TDGAAccount account)
		{
			TDGASessionController.sGameSessionId = TDGASessionController.createSessionID();
			TDGAPreference.setActiveSessionID(TDGASessionController.sGameSessionId);
			TDGAPreference.setActiveSessionStartTime(time);
			TDGAPreference.setActiveSessionEndTime(0L);
			TDGAPreference.setSessionStartCurrentTimeMillis();
			TDGAEventListManager.addEvent(new EventLogin(TDGASessionController.sGameSessionId, account, interval));
		}

		private static void saveSessionEnd(string sessionId, long start, long end)
		{
			TDGAPreference.setActiveSessionEndTime(end);
			TDGAEventListManager.addEvent(new EventLogout(sessionId, TDGAAccount.sAccount, TDGAPreference.getSessionStartCurrentTimeMillis(), end - start));
		}

		private static string createSessionID()
		{
			return MD5Core.GetHashString(Encoding.get_UTF8().GetBytes(((DateTime.get_UtcNow().get_Ticks() - new DateTime(1970, 1, 1, 0, 0, 0).get_Ticks()) / 10000L).ToString()));
		}
	}
}
