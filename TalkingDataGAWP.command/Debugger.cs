using System;
using System.Diagnostics;

namespace TalkingDataGAWP.command
{
	internal class Debugger
	{
		public enum LogLevel
		{
			Trace,
			Debug,
			Error
		}

		private static readonly Debugger.LogLevel MinLogLevel = Debugger.LogLevel.Debug;

		public static void Log(string msg)
		{
			Debugger.Log(Debugger.LogLevel.Debug, msg);
		}

		public static void Log(Debugger.LogLevel logLevel, string msg)
		{
			if (!string.IsNullOrEmpty(msg))
			{
				Debugger.LogLevel arg_0E_0 = Debugger.MinLogLevel;
			}
		}

		public static void Assert(bool condition)
		{
			Debugger.Assert(condition, "Default Failure Message.");
		}

		public static void Assert(bool condition, string msg)
		{
			if (!condition)
			{
				Debugger.Log(msg + " failed");
			}
		}

		private static string GetCaller()
		{
			StackTrace stackTrace = new StackTrace();
			StackFrame stackFrame = null;
			for (int i = 1; i < stackTrace.get_FrameCount(); i++)
			{
				if (!stackTrace.GetFrame(i).GetMethod().get_DeclaringType().get_FullName().Contains(stackTrace.GetFrame(0).GetMethod().get_DeclaringType().get_FullName()))
				{
					stackFrame = stackTrace.GetFrame(i);
					break;
				}
			}
			if (stackFrame == null)
			{
				StackTrace expr_5A = stackTrace;
				stackFrame = expr_5A.GetFrame(expr_5A.get_FrameCount() - 1);
			}
			return stackFrame.GetMethod().get_DeclaringType().get_FullName() + "." + stackFrame.GetMethod().get_Name();
		}
	}
}
