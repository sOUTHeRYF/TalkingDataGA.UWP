using Microsoft.Phone.Net.NetworkInformation;
using System;
using System.Runtime.Serialization;
using TalkingDataGAWP.controllers;

namespace TalkingDataGAWP.Entity
{
	[DataContract]
	internal class VODeviceProfile
	{
		[DataMember]
		public string deviceId = string.Empty;

		[DataMember]
		public string mobileModel = string.Empty;

		[DataMember]
		public int osSdkVersion;

		[DataMember]
		public double lng;

		[DataMember]
		public bool isJailBroken;

		[DataMember]
		public double lat;

		[DataMember]
		public string pixel = string.Empty;

		[DataMember]
		public string country = string.Empty;

		[DataMember]
		public string language = string.Empty;

		[DataMember]
		public int timezone = 8;

		[DataMember]
		public string osVersion = string.Empty;

		[DataMember]
		public string simOperator = string.Empty;

		[DataMember]
		public string networkOperator = string.Empty;

		[DataMember]
		public string manufacture = string.Empty;

		[DataMember]
		public string wap = string.Empty;

		[DataMember]
		public int cid;

		[DataMember]
		public string carrier = string.Empty;

		[DataMember]
		public string networkType = string.Empty;

		[DataMember]
		public string apnName = string.Empty;

		[DataMember]
		public string apnOperator = string.Empty;

		[DataMember]
		public bool apnProxy;

		[DataMember]
		public string tdudid = string.Empty;

		private static VODeviceProfile _instance;

		public static VODeviceProfile getInstance()
		{
			if (VODeviceProfile._instance == null)
			{
				VODeviceProfile._instance = new VODeviceProfile();
			}
			return VODeviceProfile._instance;
		}

		private VODeviceProfile()
		{
			DeviceProfileController.getLocation(out this.lat, out this.lng);
			this.carrier = DeviceNetworkInformation.get_CellularMobileOperator();
			this.deviceId = DeviceProfileController.getDeviceID();
			this.mobileModel = DeviceProfileController.getMobileModel();
			this.osSdkVersion = Environment.get_OSVersion().get_Version().get_Major();
			this.osVersion = Environment.get_OSVersion().get_Version().ToString();
			DeviceProfileController.getPixelMetric(this);
			this.networkType = DeviceProfileController.getNetStatus();
		}
	}
}
