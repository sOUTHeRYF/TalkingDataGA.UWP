using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using TalkingDataGAWP.command;
using TalkingDataGAWP.Entity;
using TalkingDataGAWP.Third.Gzip;

namespace TalkingDataGAWP.controllers
{
	internal class TDGAHttpManager
	{
		private static TDGAHttpManager _manager = new TDGAHttpManager();

		private Thread loopThread;

		private bool _sending;

		private bool loopRunning;

		public bool isSending
		{
			get
			{
				return this._sending;
			}
		}

		public static TDGAHttpManager getInstance()
		{
			return TDGAHttpManager._manager;
		}

		public void sendDataWithDetalTime()
		{
			if (this.loopThread == null || !this.loopThread.get_IsAlive())
			{
				this.loopRunning = true;
				this.loopThread = new Thread(new ThreadStart(this.threadRunLoop));
				this.loopThread.Start();
			}
		}

		public void sendDataRightNow()
		{
			new Thread(new ThreadStart(this.sendEventToServer)).Start();
		}

		public void stopSendDataWithDetalTime()
		{
			try
			{
				if (this.loopThread != null)
				{
					this.loopRunning = false;
					this.loopThread.Abort();
					this.loopThread = null;
				}
			}
			catch (Exception)
			{
			}
		}

		private void threadRunLoop()
		{
			while (this.loopRunning)
			{
				this.sendEventToServer();
				Thread.Sleep(TDConfiguration.MaxWaitTime);
			}
		}

		private void sendEventToServer()
		{
			if (!this.isSending && TDGAEventListManager.eventCount() > 0)
			{
				TDGAEventListManager.saveEventList();
				List<string> list = TDGAEventListManager.loadEventList();
				string appProfile = JsonUtil.objectToString(VOAppProfile.getInstance());
				string deviceProfile = JsonUtil.objectToString(VODeviceProfile.getInstance());
				string jsonArray = this.getJsonArray(appProfile, deviceProfile, list);
				Debugger.Log("send data to server--->" + jsonArray);
				new AsyncPostUtils().Post(new Uri(TDConfiguration.SEND_LOG_URL, 1), GZipStream.CompressString(jsonArray), new AsyncPostUtils.ResponseCallback(this.AsynPostResponseCallBack));
			}
		}

		private string getJsonArray(string appProfile, string deviceProfile, List<string> list)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{\"appProfile\":").Append(appProfile).Append(",").Append("\"deviceProfile\":").Append(deviceProfile).Append(",").Append("\"events\":");
			stringBuilder.Append("[");
			for (int i = 0; i < list.get_Count(); i++)
			{
				stringBuilder.Append(list.get_Item(i));
				if (i < list.get_Count() - 1)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append("]").Append("}");
			return stringBuilder.ToString();
		}

		public void AsynPostResponseCallBack(HttpWebResponse response)
		{
			lock (this)
			{
				this._sending = false;
				if (response != null && response.get_StatusCode() == 200)
				{
					Debugger.Log("send data to server--->success");
					TDGAEventListManager.sendEventSuccess();
				}
				else
				{
					TDGAEventListManager.sendEventFaild();
					Debugger.Log("send data to server--->faild");
				}
			}
		}
	}
}
