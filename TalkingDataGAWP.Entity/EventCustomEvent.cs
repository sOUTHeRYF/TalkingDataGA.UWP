using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TalkingDataGAWP.Entity
{
	[DataContract]
	internal class EventCustomEvent : EventBase
	{
		private string mGameSessionID;

		private string mUserID;

		private int mLevel;

		private string mGameServer;

		private string mActionID;

		private Dictionary<string, object> mActionData;

		protected override void initializeEventCustomizeMap()
		{
			base.eventDataAppendItem("gameSessionID", this.mGameSessionID).eventDataAppendItem("userID", this.mUserID).eventDataAppendItem("level", this.mLevel).eventDataAppendItem("gameServer", this.mGameServer).eventDataAppendItem("actionID", this.mActionID).eventDataAppendItem("actionData", this.mActionData);
		}

		public EventCustomEvent(string gameSessionID, TDGAAccount account, string actionID, Dictionary<string, object> actionData) : base("G8")
		{
			this.mGameSessionID = gameSessionID;
			this.mUserID = account.accountID;
			this.mLevel = account.level;
			this.mGameServer = account.gameServer;
			this.mActionID = actionID;
			this.mActionData = actionData;
			if (this.mActionData == null)
			{
				this.mActionData = new Dictionary<string, object>();
			}
		}
	}
}
