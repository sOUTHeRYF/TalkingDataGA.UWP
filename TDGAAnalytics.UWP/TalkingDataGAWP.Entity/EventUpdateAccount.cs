using System;
using System.Runtime.Serialization;

namespace TalkingDataGAWP.Entity
{
	[DataContract]
	internal class EventUpdateAccount : EventBase
	{
		private string mGameSessionID;

		private string mAccountId;

		private int mLevel;

		private int mGender;

		private string mAccount;

		private int mAccountType;

		private string mGameServer;

		private int mAge;

		public EventUpdateAccount(string gameSessionID, TDGAAccount account) : base("G7")
		{
			this.mGameSessionID = gameSessionID;
			this.mAccountId = account.accountID;
			this.mLevel = account.level;
			this.mGender = (int)account.gender;
			this.mAccountType = (int)account.accountType;
			this.mGameServer = account.gameServer;
			this.mAccount = account.accountName;
			this.mAge = account.age;
		}

		protected override void initializeEventCustomizeMap()
		{
			base.eventDataAppendItem("gameSessionID", this.mGameSessionID).eventDataAppendItem("userID", this.mAccountId).eventDataAppendItem("level", this.mLevel).eventDataAppendItem("sex", this.mGender).eventDataAppendItem("account", this.mAccount).eventDataAppendItem("accountType", this.mAccountType).eventDataAppendItem("gameServer", this.mGameServer).eventDataAppendItem("age", this.mAge);
		}
	}
}
