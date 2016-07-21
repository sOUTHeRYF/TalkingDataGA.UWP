using System;

namespace TalkingDataGAWP.command
{
	internal class Constants
	{
		internal static bool debug_mode = true;

		internal static readonly string SDK_TYPE = "wphone+";

		internal static readonly string SDK_VERSION = "2.0.7";

		internal static readonly string SEND_LOG_URL = "http://gf.tenddata.com/g/d";

		internal static TimeSpan sessionContinueInterval = new TimeSpan(0, 0, 30);

		internal static readonly string PartnerId = "TD";

		internal static int COARSE_PACKAGE_SIZE = 81920;

		internal static int File_PACKAGE_SIZE = 1048576;

		internal static TimeSpan MaxWaitTime = new TimeSpan(0, 0, 3, 0);

		public const string PUBLISH_CHANNEL = "TDGA";

		public const string GAME_SESSIONID = "gameSessionID";

		public const string USER_ID = "userID";

		public const string LEVEL = "level";

		public const string SEX = "sex";

		public const string ACCOUNT_TYPE = "accountType";

		public const string ACCOUNT_NAME = "account";

		public const string GAME_SERVER = "gameServer";

		public const string AGE = "age";

		public const string PRE_LEVEL = "preLevel";

		public const string GAME_SESSION_START = "gameSessionStart";

		public const string DURATION = "duration";

		public const string VIRTUAL_CURRENCY = "virtualCurrencyAmount";

		public const string VIRTUAL_CURRENCY_TYPE = "currencyType";

		public const string REWARD_REASON = "reason";

		public const string INTERVAL = "interval";

		public const string MISSION_ID = "mission";

		public const string STATUS = "status";

		public const string CAUSE = "cause";

		public const string TIME_CONSUMING = "timeConsuming";

		public const string CUSTOMER_ACTION_ID = "actionID";

		public const string CUSTOMER_ACTION_DATA = "actionData";

		public const string ORDER_ID = "orderId";

		public const string IAP_ID = "iapId";

		public const string CURRENCY_AMOUNT = "currencyAmount";

		public const string PAYMENT_TYPE = "paymentType";

		public const string ITEM_ID = "itemid";

		public const string ITEM_NUMBER = "itemnumber";
	}
}
