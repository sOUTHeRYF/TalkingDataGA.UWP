using System;
using TalkingDataGAWP.command;
using TalkingDataGAWP.controllers;
using TalkingDataGAWP.Entity;

namespace TalkingDataGAWP
{
	public class TDGAMission
	{
		public static string sMissionId = "";

		public static void onBegin(string missionId)
		{
			if (!TalkingDataGA.isAlreadyInit)
			{
				Debugger.Log("SDK not initialized. TDGAMission.onBegin()");
				return;
			}
			TDGAEventListManager.addEvent(new EventMission(TDGASessionController.sGameSessionId, TDGAAccount.sAccount, missionId, string.Empty, 0L, EventMission.MissionStatus.START));
			TDGAPreference.setMissionID(missionId);
			TDGAAccount.sAccount.setGameDurationByMissionStart(missionId);
			TDGAMission.sMissionId = missionId;
		}

		public static void onCompleted(string missionId)
		{
			if (!TalkingDataGA.isAlreadyInit)
			{
				Debugger.Log("SDK not initialized. TDGAMission.onCompleted()");
				return;
			}
			long num = (TDGAAccount.sAccount == null) ? 0L : TDGAAccount.sAccount.getMissionDuration(missionId);
			TDGAEventListManager.addEvent(new EventMission(TDGASessionController.sGameSessionId, TDGAAccount.sAccount, missionId, string.Empty, num / 1000L, EventMission.MissionStatus.COMPLETED));
			TDGAMission.sMissionId = string.Empty;
			TDGAPreference.setMissionID(TDGAMission.sMissionId);
		}

		public static void onFailed(string missionId, string cause)
		{
			if (!TalkingDataGA.isAlreadyInit)
			{
				Debugger.Log("SDK not initialized. TDGAMission.onFailed()");
				return;
			}
			long timeConsuming = (TDGAAccount.sAccount == null) ? 0L : TDGAAccount.sAccount.getMissionDuration(missionId);
			TDGAEventListManager.addEvent(new EventMission(TDGASessionController.sGameSessionId, TDGAAccount.sAccount, missionId, cause, timeConsuming, EventMission.MissionStatus.FAILED));
			TDGAMission.sMissionId = "";
			TDGAPreference.setMissionID(TDGAMission.sMissionId);
		}
	}
}
