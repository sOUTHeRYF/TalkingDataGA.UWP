using Microsoft.Phone.Info;
using Microsoft.Phone.Net.NetworkInformation;
using System;
using System.Globalization;
using System.Net.NetworkInformation;

namespace TalkingDataGAWP.command
{
	internal class DeviceProfileUtil
	{
		public static string getNetStatus()
		{
			string result = "unknown";
			NetworkInterfaceType networkInterfaceType = NetworkInterface.get_NetworkInterfaceType();
			if (networkInterfaceType <= 71)
			{
				if (networkInterfaceType != null)
				{
					if (networkInterfaceType == 71)
					{
						result = "wifi";
					}
				}
				else
				{
					result = "offline";
				}
			}
			else if (networkInterfaceType != 145)
			{
				if (networkInterfaceType == 146)
				{
					result = "3G";
				}
			}
			else
			{
				result = "2G";
			}
			return result;
		}

		public static string getFirewareVersion()
		{
			string result = string.Empty;
			if (SDKTYPE.isSDKFor_WP8())
			{
				result = DeviceStatus.get_DeviceFirmwareVersion();
			}
			else
			{
				result = DeviceExtendedProperties.GetValue("DeviceFirmwareVersion").ToString();
			}
			return result;
		}

		public static string[] getCulture()
		{
			string[] array = new string[]
			{
				"unknown",
				"unknown"
			};
			try
			{
				string[] array2 = CultureInfo.get_CurrentCulture().ToString().Split(new char[]
				{
					'-'
				});
				if (array2 != null && array2.Length == 2)
				{
					array[0] = array2[0];
					array[1] = array2[1];
				}
			}
			catch (Exception ex)
			{
				Debugger.Log("Fail to read culture :" + ex.get_Message());
			}
			return array;
		}

		public static int GetDSTAdjustedTimeZone()
		{
			int result;
			try
			{
				int num = 0;
				int.TryParse(TimeZoneInfo.get_Local().get_DisplayName().Split(new char[]
				{
					' '
				})[0].Split(new char[]
				{
					':'
				})[0].Substring(3), ref num);
				if (TimeZoneInfo.get_Local().IsDaylightSavingTime(DateTime.get_Now()))
				{
					num++;
				}
				result = Math.Abs(num);
			}
			catch (Exception ex)
			{
				Debugger.Log("Fail to read timezone (default 8) :" + ex.get_Message());
				result = 8;
			}
			return result;
		}

		public int getCurrentNetState()
		{
			if (!NetworkInterface.GetIsNetworkAvailable())
			{
				return -1;
			}
			return 1;
		}

		public static string getANID()
		{
			string text = string.Empty;
			object obj;
			if (UserExtendedProperties.TryGetValue("ANID", ref obj))
			{
				text += obj;
			}
			object obj2;
			if (UserExtendedProperties.TryGetValue("ANID2", ref obj2))
			{
				if (text.Trim().get_Length() > 0)
				{
					text += "+";
				}
				text += obj2;
			}
			return text;
		}
	}
}
