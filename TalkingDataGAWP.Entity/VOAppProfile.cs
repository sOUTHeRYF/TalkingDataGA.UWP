using System;
using System.Runtime.Serialization;
using TalkingDataGAWP.command;

namespace TalkingDataGAWP.Entity
{
	[DataContract]
	internal class VOAppProfile
	{
		private static VOAppProfile instance;

		[DataMember]
		public string sequenceNumber
		{
			get;
			set;
		}

		[DataMember]
		public string appPackageName
		{
			get;
			set;
		}

		[DataMember]
		public string appVersionName
		{
			get;
			set;
		}

		[DataMember]
		public string sdkVersion
		{
			get;
			set;
		}

		[DataMember]
		public string sdkType
		{
			get;
			set;
		}

		[DataMember]
		public string partner
		{
			get;
			set;
		}

		[DataMember]
		public string appDisplayName
		{
			get;
			set;
		}

		[DataMember]
		public bool isCracked
		{
			get;
			set;
		}

		[DataMember]
		public long installationTime
		{
			get;
			set;
		}

		[DataMember]
		public long purchaseTime
		{
			get;
			set;
		}

		private VOAppProfile()
		{
			this.appPackageName = AppProfileUtil.GetPackageName();
			this.appVersionName = AppProfileUtil.GetAPPPropery("Version");
			this.installationTime = AppProfileUtil.GetInstallionTime();
			this.appDisplayName = AppProfileUtil.GetAPPPropery("Title");
			this.sdkType = "wp_Native_SDK";
			this.sdkVersion = Constants.SDK_VERSION;
			this.isCracked = AppProfileUtil.isCrack();
		}

		public static VOAppProfile getInstance()
		{
			if (VOAppProfile.instance == null)
			{
				VOAppProfile.instance = new VOAppProfile();
			}
			return VOAppProfile.instance;
		}

		public string getJsonString()
		{
			return JsonUtil.objectToString(this);
		}
	}
}
