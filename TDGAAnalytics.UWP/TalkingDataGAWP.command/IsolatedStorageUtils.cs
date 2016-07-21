using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TalkingDataGAWP.command
{
	internal class IsolatedStorageUtils
	{
		[CompilerGenerated]
	//	[Serializable]
		private sealed class IsolatedStorageUtilsInnerClass
        {
			public static readonly IsolatedStorageUtils.IsolatedStorageUtilsInnerClass Instance = new IsolatedStorageUtils.IsolatedStorageUtilsInnerClass();

			public static Func<string, string> FuncA;

			internal string GetAllFileNames(string x)
			{
				return x;
			}
		}

		private const bool MaskExceptions = true;

		public static bool fileExists(string path)
		{
			return IsolatedStorageFile.GetUserStoreForApplication().FileExists(path);
		}

		public static void CreateDirectoryIfNotFound(string path)
		{
			if (!IsolatedStorageFile.GetUserStoreForApplication().DirectoryExists(path))
			{
				Debugger.Log("Create Directory:" + path);
				IsolatedStorageFile.GetUserStoreForApplication().CreateDirectory(path);
			}
		}

		public static string[] GetAllFileNames(string path)
		{
			return IsolatedStorageUtils.GetAllFileNames(path, null);
		}

		public static string[] GetAllFileNames(string path, string filePrefix)
		{
			if (path != null && !path.EndsWith("\\"))
			{
				path += "\\";
			}
			if (!IsolatedStorageFile.GetUserStoreForApplication().DirectoryExists(path))
			{
				return new string[0];
			}
			path += "*";
			IEnumerable<string> enumerable = (!string.IsNullOrEmpty(path)) ? IsolatedStorageFile.GetUserStoreForApplication().GetFileNames(path) : IsolatedStorageFile.GetUserStoreForApplication().GetFileNames();
			if (!string.IsNullOrEmpty(filePrefix))
			{
				enumerable = Enumerable.Where<string>(enumerable, (string fileName) => fileName.StartsWith(filePrefix));
			}
			IEnumerable<string> arg_AB_0 = enumerable;
			Func<string, string> arg_AB_1;
			if ((arg_AB_1 = IsolatedStorageUtils.IsolatedStorageUtilsInnerClass.FuncA) == null)
			{
				arg_AB_1 = (IsolatedStorageUtils.IsolatedStorageUtilsInnerClass.FuncA = new Func<string, string>(IsolatedStorageUtils.IsolatedStorageUtilsInnerClass.Instance.GetAllFileNames));
			}
			string[] array = Enumerable.ToArray<string>(Enumerable.OrderBy<string, string>(arg_AB_0, arg_AB_1));
			Debugger.Log(string.Concat(new object[]
			{
				"GetAllFileNames. Count:",
				Enumerable.Count<string>(array),
				". Files:",
				string.Join(",", array)
			}));
			return array;
		}

		public static T Get<T>(string fileName)
		{
			return IsolatedStorageUtils.Get<T>(null, fileName);
		}

		public static T Get<T>(string path, string fileName)
		{
			if (path != null)
			{
				fileName = path + "\\" + fileName;
			}
			Type typeFromHandle = typeof(IsolatedStorageUtils);
			T result;
			lock (typeFromHandle)
			{
				if (!IsolatedStorageFile.GetUserStoreForApplication().FileExists(fileName))
				{
					result = default(T);
				}
				else
				{
					using (IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream(fileName, FileMode.Open, IsolatedStorageFile.GetUserStoreForApplication()))
					{
						result = IsolatedStorageUtils.GetObject<T>(isolatedStorageFileStream);
					}
				}
			}
			return result;
		}

		public static List<T> GetAll<T>(string path, string filePrefix)
		{
			List<T> list = new List<T>();
			Type typeFromHandle = typeof(IsolatedStorageUtils);
			lock (typeFromHandle)
			{
				string[] allFileNames = IsolatedStorageUtils.GetAllFileNames(path, filePrefix);
				for (int i = 0; i < allFileNames.Length; i++)
				{
					string fileName = allFileNames[i];
					T t = IsolatedStorageUtils.Get<T>(path, fileName);
					if (t != null)
					{
						list.Add(t);
					}
				}
			}
			return list;
		}

		public static byte[] GetBinaryData(string fileName)
		{
			return IsolatedStorageUtils.GetBinaryData(null, fileName);
		}

		public static byte[] GetBinaryData(string path, string fileName)
		{
			if (path != null)
			{
				fileName = path + "\\" + fileName;
			}
			Type typeFromHandle = typeof(IsolatedStorageUtils);
			byte[] result;
			lock (typeFromHandle)
			{
				if (!IsolatedStorageFile.GetUserStoreForApplication().FileExists(fileName))
				{
					result = null;
				}
				else
				{
					using (IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream(fileName, FileMode.Open, IsolatedStorageFile.GetUserStoreForApplication()))
					{
						if (isolatedStorageFileStream == null)
						{
							result = null;
						}
						else
						{
							using (BinaryReader binaryReader = new BinaryReader(isolatedStorageFileStream))
							{
								result = binaryReader.ReadBytes((int)isolatedStorageFileStream.Length);
							}
						}
					}
				}
			}
			return result;
		}

		private static T GetObject<T>(Stream stream)
		{
			T result;
			if (stream == null)
			{
				result = default(T);
				return result;
			}
			try
			{
				byte[] array = new byte[stream.Length];
				int num = 0;
				int i = array.Length;
				while (i > 0)
				{
					int num2 = stream.Read(array, num, i);
					if (num2 <= 0)
					{
						break;
					}
					i -= num2;
					num += num2;
				}
				result = JsonUtil.bytesToObject<T>(array);
			}
			catch (Exception ex)
			{
				Debugger.Log(Debugger.LogLevel.Error, "Failed to deserialize object. Mask exception and proceed. " + ex.StackTrace);
				result = default(T);
			}
			return result;
		}

		public static void Save<T>(string fileName, T obj)
		{
			IsolatedStorageUtils.Save<T>(null, fileName, obj);
		}

		public static void Save<T>(string path, string fileName, T obj)
		{
			IsolatedStorageUtils.CreateDirectoryIfNotFound("TalkingDataGA");
			Debugger.Log("Save. FileName:" + fileName + ". Path:" + path);
			if (!string.IsNullOrEmpty(path))
			{
				fileName = path + "\\" + fileName;
			}
			Type typeFromHandle = typeof(IsolatedStorageUtils);
			lock (typeFromHandle)
			{
				if (obj == null)
				{
					if (IsolatedStorageFile.GetUserStoreForApplication().FileExists(fileName))
					{
						IsolatedStorageFile.GetUserStoreForApplication().DeleteFile(fileName);
					}
				}
				else
				{
					using (IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream(fileName, FileMode.Create, IsolatedStorageFile.GetUserStoreForApplication()))
					{
						try
						{
							byte[] array = JsonUtil.objectToBytes(obj);
							JsonUtil.objectToString(obj);
							isolatedStorageFileStream.Write(array, 0, array.Length);
							isolatedStorageFileStream.Flush();
						}
						catch (Exception ex)
						{
							Debugger.Log(Debugger.LogLevel.Error, "Failed to save object. Mask exception and proceed. " + ex.StackTrace);
							if (isolatedStorageFileStream != null)
							{
								isolatedStorageFileStream.Dispose();
							}
						}
					}
				}
			}
		}

		public static void Delete(string fileName)
		{
			IsolatedStorageUtils.Delete(null, fileName);
		}

		public static void Delete(string path, string fileName)
		{
			Debugger.Log("Delete. FileName:" + fileName + ". Path:" + path);
			if (!string.IsNullOrEmpty(path))
			{
				fileName = path + "\\" + fileName;
			}
			if (IsolatedStorageFile.GetUserStoreForApplication().FileExists(fileName))
			{
				IsolatedStorageFile.GetUserStoreForApplication().DeleteFile(fileName);
			}
		}
	}
}
