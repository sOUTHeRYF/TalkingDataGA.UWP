using System;
using System.Runtime.Serialization;

namespace TalkingDataGAWP.Entity
{
	[DataContract]
	internal class EventMission : EventBase
	{
		public enum MissionStatus
		{
			START = 1,
			COMPLETED,
			FAILED
		}

		private string mGameSessionID;

		private string mUserID;

		private int mLevel;

		private string mMission;

		private string mGameServer;

		private EventMission.MissionStatus mStatus;

		private string mCause;

		private long mTimeConsuming;

		protected override void initializeEventCustomizeMap()
		{
			base.eventDataAppendItem("gameSessionID", this.mGameSessionID).eventDataAppendItem("userID", this.mUserID).eventDataAppendItem("level", this.mLevel).eventDataAppendItem("gameServer", this.mGameServer).eventDataAppendItem("mission", this.mMission).eventDataAppendItem("cause", this.mCause).eventDataAppendItem("status", (int)this.mStatus).eventDataAppendItem("timeConsuming", this.mTimeConsuming);
		}

		public EventMission(string gameSessionID, TDGAAccount account, string mission, string cause, long timeConsuming, EventMission.MissionStatus status) : base("G6")
		{
			this.mGameSessionID = gameSessionID;
			this.mUserID = account.accountID;
			this.mLevel = account.level;
			this.mGameServer = account.gameServer;
			this.mMission = mission;
			this.mCause = cause;
			this.mStatus = status;
			this.mTimeConsuming = timeConsuming;
		}
	}
}
