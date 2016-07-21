using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace TalkingDataGAWP.command
{
	internal class SerializationUtil
	{
		public static T bytesToObject<T>(byte[] array)
		{
			T result;
			using (MemoryStream memoryStream = new MemoryStream(array))
			{
				result = (T)((object)new DataContractJsonSerializer(typeof(T)).ReadObject(memoryStream));
			}
			return result;
		}

		public static byte[] objectToBytes(object obj)
		{
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
			byte[] array = SerializationUtil.objectToBytes(obj);
			return Encoding.get_UTF8().GetString(array, 0, array.Length);
		}

		public static T stringToObject<T>(string str)
		{
			return SerializationUtil.bytesToObject<T>(Encoding.get_UTF8().GetBytes(str));
		}
	}
}
