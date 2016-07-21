using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace TalkingDataGAWP.command
{
	internal class JsonUtil
	{
		public static T bytesToObject<T>(byte[] array)
		{
			T result;
			if (array == null || array.Length == 0)
			{
				result = default(T);
				return result;
			}
			using (MemoryStream memoryStream = new MemoryStream(array))
			{
				result = (T)((object)new DataContractJsonSerializer(typeof(T)).ReadObject(memoryStream));
			}
			return result;
		}

		public static byte[] objectToBytes(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new DataContractJsonSerializer(obj.GetType()).WriteObject(memoryStream, obj);
				result = memoryStream.ToArray();
			}
			return result;
		}

		public static string objectToString(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			byte[] array = JsonUtil.objectToBytes(obj);
			return Encoding.get_UTF8().GetString(array, 0, array.Length);
		}

		public static T stringToObject<T>(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return default(T);
			}
			return JsonUtil.bytesToObject<T>(Encoding.get_UTF8().GetBytes(str));
		}
	}
}
