using System;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace TalkingDataGAWP.command
{
	internal class AppProfileUtil
	{
		public static string GetPackageName()
		{
			string result;
			try
			{
				result = Assembly.GetCallingAssembly().GetName().get_Name();
			}
			catch (Exception ex)
			{
				Debugger.Log("GetPackageName: " + ex.get_Message());
				result = "Un_known";
			}
			return result;
		}

		public static string GetAPPPropery(string Key)
		{
			try
			{
				XDocument xDocument = XDocument.Load("WMAppManifest.xml");
				if (xDocument != null)
				{
					using (XmlReader xmlReader = xDocument.CreateReader(0))
					{
						xmlReader.ReadToDescendant("App");
						string result;
						if (!xmlReader.IsStartElement())
						{
							result = null;
							return result;
						}
						result = xmlReader.GetAttribute(Key);
						return result;
					}
				}
			}
			catch (Exception ex)
			{
				Debugger.Log("Fail to read App version from WMAppManifest : " + ex.get_Message());
			}
			return null;
		}

		public static bool isCrack()
		{
			return false;
		}

		public static long GetPurchaseTime()
		{
			return 0L;
		}

		public static long GetInstallionTime()
		{
			return 0L;
		}

		public static string getAppVersion()
		{
			string result;
			try
			{
				result = Assembly.GetCallingAssembly().GetName().get_Version().ToString();
			}
			catch (Exception ex)
			{
				Debugger.Log("sdkVersion: " + ex.get_Message());
				result = "-1.0.0";
			}
			return result;
		}
	}
}
