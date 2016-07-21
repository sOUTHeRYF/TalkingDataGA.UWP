using System;

namespace TalkingDataGAWP.command
{
	internal class TDConfiguration
	{
		public static string AppKey = string.Empty;

		public static string appChannel = string.Empty;

		public static bool isNeedReportException = false;

		public const string BaseDir = "TalkingDataGA";

		internal static bool debug_mode = true;

		internal static readonly string SDK_TYPE = SDKTYPE.wphone_8;

		internal static readonly string SEND_LOG_URL = "http://gaandroid.talkingdata.net/g/d?crc=";

		internal static TimeSpan sessionContinueInterval = new TimeSpan(0, 0, 30);

		internal static int COARSE_PACKAGE_SIZE = 81920;

		internal static int File_PACKAGE_SIZE = 1048576;

		internal static readonly int MaxEventCount = 1000;

		internal static TimeSpan MaxWaitTime = new TimeSpan(0, 0, 3, 0);
	}
}
