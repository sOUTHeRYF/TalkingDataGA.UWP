//using Microsoft.Phone.Info;
//using Microsoft.Phone.Net.NetworkInformation;
using System;
using System.Globalization;
using System.Net.NetworkInformation;

using Windows.Networking.Connectivity;
namespace TalkingDataGAWP.command
{
	internal class DeviceProfileUtil
	{
		public static string getNetStatus()
		{
			string result = "unknown";
            ConnectionProfile internetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
            if(internetConnectionProfile != null)
            {
                uint ianaInterfaceType = internetConnectionProfile.NetworkAdapter.IanaInterfaceType;
                if (ianaInterfaceType <= 71u)
                {
                    if (ianaInterfaceType != 6u)
                    {
                        if (ianaInterfaceType == 71u)
                        {
                            result = "wifi";
                        }
                    }
                    else
                    {
                        result = "offline";
                    }
                }
                else if (ianaInterfaceType != 216u)
                {
                    switch (ianaInterfaceType)
                    {
                        case 243u:
                        case 244u:
                            result = "3G";
                            break;
                    }
                }
                else
                {
                    result = "2G";
                }
            }
			return result;
		}

		public static string getFirewareVersion()
		{
			string result = string.Empty;
            /*
			if (SDKTYPE.isSDKFor_WP8())
			{
				result = DeviceStatus.get_DeviceFirmwareVersion();
			}
			else
			{
				result = DeviceExtendedProperties.GetValue("DeviceFirmwareVersion").ToString();
			}*/
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
				string[] array2 = CultureInfo.CurrentCulture.ToString().Split(new char[]
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
				Debugger.Log("Fail to read culture :" + ex.Message);
			}
			return array;
		}

		public static int GetDSTAdjustedTimeZone()
		{
			int result;
			try
			{
				int num = 0;
				int.TryParse(TimeZoneInfo.Local.DisplayName.Split(new char[]
				{
					' '
				})[0].Split(new char[]
				{
					':'
				})[0].Substring(3), out num);
				if (TimeZoneInfo.Local.IsDaylightSavingTime(DateTime.Now))
				{
					num++;
				}
				result = Math.Abs(num);
			}
			catch (Exception ex)
			{
				Debugger.Log("Fail to read timezone (default 8) :" + ex.Message);
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
            /*
			object obj;
			if (UserExtendedProperties.TryGetValue("ANID", ref obj))
			{
				text += obj;
			}
			object obj2;
			if (UserExtendedProperties.TryGetValue("ANID2", ref obj2))
			{
				if (text.Trim().Length > 0)
				{
					text += "+";
				}
				text += obj2;
			}*/
			return text;
		}
	}
}
