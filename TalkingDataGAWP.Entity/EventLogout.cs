using System;
using System.Runtime.Serialization;

namespace TalkingDataGAWP.Entity
{
	[DataContract]
	internal class EventLogout : EventBase
	{
		private string mGameSessionID;

		private string mUserID;

		private long mSessionStartTime;

		private long mSessionDuration;

		private int mLevel;

		private string mGameServer;

		public EventLogout(string gameSessionID, TDGAAccount account, long start, long dur) : base("G4")
		{
			this.mGameSessionID = gameSessionID;
			this.mUserID = account.accountID;
			this.mLevel = account.level;
			this.mGameServer = account.gameServer;
			this.mSessionStartTime = start;
			this.mSessionDuration = dur;
		}

		protected override void initializeEventCustomizeMap()
		{
			base.eventDataAppendItem("gameSessionID", this.mGameSessionID).eventDataAppendItem("userID", this.mUserID).eventDataAppendItem("level", this.mLevel).eventDataAppendItem("gameServer", this.mGameServer).eventDataAppendItem("gameSessionStart", this.mSessionStartTime).eventDataAppendItem("duration", this.mSessionDuration / 1000L);
		}
	}
}
