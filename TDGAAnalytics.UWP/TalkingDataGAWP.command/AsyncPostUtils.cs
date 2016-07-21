using System;
using System.IO;
using System.Net;

namespace TalkingDataGAWP.command
{
	internal class AsyncPostUtils
	{
		public delegate void ResponseCallback(HttpWebResponse response);

		private byte[] _requestData;

		private AsyncPostUtils.ResponseCallback _responseCallBack;

		public void Post(Uri uri, byte[] requestData, AsyncPostUtils.ResponseCallback responseCallBack)
		{
			Debugger.Log(string.Concat(new object[]
			{
				"Start Post. URI:",
				uri.get_AbsoluteUri(),
				". RequestDataSize:",
				requestData.Length
			}));
			this._requestData = requestData;
			this._responseCallBack = responseCallBack;
			HttpWebRequest httpWebRequest = WebRequest.Create(uri) as HttpWebRequest;
			httpWebRequest.set_Method("POST");
			httpWebRequest.set_ContentType("application/octet-stream");
			httpWebRequest.BeginGetRequestStream(new AsyncCallback(this.RequestStreamCallback), httpWebRequest);
		}

		private void RequestStreamCallback(IAsyncResult ar)
		{
			Debugger.Log("Start writing request data.");
			HttpWebRequest httpWebRequest = (HttpWebRequest)ar.get_AsyncState();
			BinaryWriter expr_22 = new BinaryWriter(httpWebRequest.EndGetRequestStream(ar));
			expr_22.Write(this._requestData);
			expr_22.Dispose();
			Debugger.Log("Call BeginGetResponse.");
			httpWebRequest.BeginGetResponse(new AsyncCallback(this.ResponseCallbackEvent), httpWebRequest);
		}

		private void ResponseCallbackEvent(IAsyncResult ar)
		{
			Debugger.Log("Received response callback. Process response.");
			HttpWebRequest httpWebRequest = ar.get_AsyncState() as HttpWebRequest;
			HttpWebResponse httpWebResponse = null;
			try
			{
				httpWebResponse = (httpWebRequest.EndGetResponse(ar) as HttpWebResponse);
				if (httpWebResponse != null && httpWebResponse.get_StatusCode() == 200)
				{
					Debugger.Log("Post successful.");
				}
				else
				{
					Debugger.Log("Post failed. Status:" + ((httpWebResponse != null) ? httpWebResponse.get_StatusCode().ToString() : "") + ". Response:" + ((httpWebResponse != null) ? httpWebResponse.ToString() : ""));
				}
			}
			catch (WebException ex)
			{
				Debugger.Log("Post Failed. Exception:" + ex.StackTrace);
			}
			catch (Exception ex2)
			{
				Debugger.Log("Post Failed. Exception:" + ex2.StackTrace);
			}
			Debugger.Log("Call ResponseCallBack.");
			this._responseCallBack(httpWebResponse);
		}
	}
}
