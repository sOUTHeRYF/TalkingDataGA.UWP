using System;

namespace TalkingDataGAWP.command
{
	internal class DateTimeUtils
	{
		public static long getCurrentTime()
		{
			return (long)(DateTime.Now - DateTime.Parse("1970-1-1")).get_TotalMilliseconds();
		}
	}
}
