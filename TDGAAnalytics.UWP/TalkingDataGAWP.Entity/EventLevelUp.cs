using System;
using System.Runtime.Serialization;

namespace TalkingDataGAWP.Entity
{
	[DataContract]
	internal class EventLevelUp : EventBase
	{
		private string mGameSessionID;

		private string mUserID;

		private int mPreLevel;

		private int mLevel;

		private string mGameServer;

		private string mMission;

		private long mTimeConsuming;

		public EventLevelUp(string sessionId, TDGAAccount account, string mission, int preLevel, long timeConsuming) : base("G5")
		{
			this.mGameSessionID = sessionId;
			this.mUserID = account.accountID;
			this.mLevel = account.level;
			this.mGameServer = account.gameServer;
			this.mMission = mission;
			this.mTimeConsuming = timeConsuming;
			this.mPreLevel = preLevel;
		}

		protected override void initializeEventCustomizeMap()
		{
			base.eventDataAppendItem("gameSessionID", this.mGameSessionID).eventDataAppendItem("userID", this.mUserID).eventDataAppendItem("level", this.mLevel).eventDataAppendItem("gameServer", this.mGameServer).eventDataAppendItem("mission", this.mMission).eventDataAppendItem("preLevel", this.mPreLevel).eventDataAppendItem("timeConsuming", this.mTimeConsuming / 1000L);
		}
	}
}
