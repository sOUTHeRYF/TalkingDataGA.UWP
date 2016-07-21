using System;
using System.Runtime.Serialization;

namespace TalkingDataGAWP.Entity
{
	[DataContract]
	internal class EventCharge : EventBase
	{
		public enum ChargeStatus
		{
			REQUEST = 1,
			SUCCESS
		}

		private string mGameSessionID;

		private string mUserID;

		private int mLevel;

		private string mGameServer;

		private string mOrderId;

		private string mIapId;

		private double mCurrencyAmount;

		private string mCurrencyType;

		private double mVirtualCurrency;

		private string mPaymentType;

		private EventCharge.ChargeStatus mStatus;

		private string mMission;

		private bool isInitialize;

		protected override void initializeEventCustomizeMap()
		{
			if (!this.isInitialize)
			{
				base.eventDataAppendItem("gameSessionID", this.mGameSessionID).eventDataAppendItem("userID", this.mUserID).eventDataAppendItem("level", this.mLevel).eventDataAppendItem("gameServer", this.mGameServer).eventDataAppendItem("mission", this.mMission).eventDataAppendItem("orderId", this.mOrderId).eventDataAppendItem("iapId", this.mIapId).eventDataAppendItem("currencyAmount", this.mCurrencyAmount).eventDataAppendItem("virtualCurrencyAmount", this.mVirtualCurrency).eventDataAppendItem("currencyType", this.mCurrencyType).eventDataAppendItem("paymentType", this.mPaymentType).eventDataAppendItem("status", (int)this.mStatus);
				this.isInitialize = true;
				return;
			}
			base.eventDic.set_Item("status", 2);
		}

		public EventCharge(string sessionId, TDGAAccount account, string mission, string orderId, string iapId, double currencyAmount, string currencyType, double virtualCurrency, string paymentType, EventCharge.ChargeStatus status) : base("G9")
		{
			this.mGameSessionID = sessionId;
			this.mUserID = account.accountID;
			this.mLevel = account.level;
			this.mGameServer = account.gameServer;
			this.mMission = mission;
			this.mOrderId = orderId;
			this.mIapId = iapId;
			this.mCurrencyAmount = currencyAmount;
			this.mCurrencyType = currencyType;
			this.mVirtualCurrency = virtualCurrency;
			this.mPaymentType = paymentType;
			this.mStatus = status;
		}
	}
}
