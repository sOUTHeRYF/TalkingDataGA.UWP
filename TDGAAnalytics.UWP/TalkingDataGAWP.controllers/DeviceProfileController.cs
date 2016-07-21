using Microsoft.Phone.Info;
using Microsoft.Phone.Net.NetworkInformation;
using System;
//using System.Device.Location;
using System.Text;
using System.Windows;
using TalkingDataGAWP.command;
using TalkingDataGAWP.Entity;

using Windows.Networking.Connectivity;
namespace TalkingDataGAWP.controllers
{
	internal class DeviceProfileController
	{
		public static string getNetStatus()
		{
            string result = "unknown";
            ConnectionProfile internetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
            if (internetConnectionProfile != null)
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

		public static void getLocation(out double lat, out double lng)
		{
			lat = 0.0;
			lng = 0.0;
			try
			{
				GeoCoordinateWatcher geoCoordinateWatcher = new GeoCoordinateWatcher(0);
				if (geoCoordinateWatcher.get_Permission() != 1)
				{
					Debugger.Log("missing permission ID_CAP_LOCATION");
				}
				else
				{
					GeoCoordinate location = geoCoordinateWatcher.get_Position().get_Location();
					if (!location.get_IsUnknown())
					{
						lat = location.get_Latitude();
						lng = location.get_Latitude();
					}
				}
			}
			catch (Exception arg_6E_0)
			{
				lat = 0.0;
				lng = 0.0;
				Debugger.Log(arg_6E_0.Message);
				Debugger.Log("maybe missing permission ID_CAP_LOCATION");
			}
		}

		public static string getDeviceID()
		{
			string text = TDGAPreference.getDeviceID();
			if (text != null && text.Length > 10)
			{
				return text;
			}
			try
			{
				byte[] array = (byte[])DeviceExtendedProperties.GetValue("DeviceUniqueId");
				if (array != null && array.Length != 0)
				{
					text = MD5Core.GetHashString(array);
				}
			}
			catch (Exception arg_36_0)
			{
				Debugger.Log(arg_36_0.Message);
				Debugger.Log("maybe missing permission ID_CAP_IDENTITY_DEVICE");
			}
			string text2 = DateTime.get_UtcNow().get_Millisecond() + DateTime.Now.get_Ticks().ToString();
			while (text.Length < 32)
			{
				Random random = new Random();
				text = text2 + random.Next(0, 32 - text2.Length + 1);
			}
			TDGAPreference.setDeviceID(text);
			return text;
		}

		public static string getMobileModel()
		{
			return DeviceStatus.get_DeviceManufacturer() + "+" + DeviceStatus.get_DeviceName();
		}

		public static void getPixelMetric(VODeviceProfile device)
		{
		//	Deployment.get_Current().get_Dispatcher().BeginInvoke(delegate
		//	{
				try
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(Application.get_Current().get_Host().get_Content().get_ActualWidth().ToString());
					stringBuilder.Append("*");
					stringBuilder.Append(Application.get_Current().get_Host().get_Content().get_ActualHeight().ToString());
					device.pixel = stringBuilder.ToString();
				}
				catch (Exception ex)
				{
					Debugger.Log("Fail to read resolution :" + ex.Message);
				}
				finally
				{
				}
	//		});
		}
	}
}
