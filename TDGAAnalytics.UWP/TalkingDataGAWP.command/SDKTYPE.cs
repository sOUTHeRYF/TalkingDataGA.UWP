using System;

namespace TalkingDataGAWP.command
{
	internal class SDKTYPE
	{
		public static string wphone_7 = "wphone+";

		public static string wphone_8 = "wphone+";

		public static bool isSDKFor_WP8()
		{
			return TDConfiguration.SDK_TYPE.Equals(SDKTYPE.wphone_8);
		}
	}
}
