using System;
using System.Runtime.Serialization;

namespace TalkingDataGAWP.Entity
{
	[DataContract]
	internal abstract class EventBase
	{
		[DataMember]
		public string eventID = string.Empty;

		[DataMember]
		public long eventOccurTime;

		protected MyJsonDic eventDic
		{
			get;
			set;
		}

		public EventBase(string eventID)
		{
			this.eventID = eventID;
			this.eventOccurTime = (DateTime.get_UtcNow().get_Ticks() - new DateTime(1970, 1, 1, 0, 0, 0).get_Ticks()) / 10000L;
			this.eventDic = new MyJsonDic();
		}

		public EventBase eventDataAppendItem(string key, object value)
		{
			if (value != null)
			{
				this.eventDic.Add(key, value);
			}
			return this;
		}

		protected abstract void initializeEventCustomizeMap();

		public bool isEventCountable()
		{
			return false;
		}

		public string toJsonString()
		{
			this.initializeEventCustomizeMap();
			return string.Concat(new object[]
			{
				"{\"eventID\":\"",
				this.eventID,
				"\",\"eventOccurTime\":",
				this.eventOccurTime,
				",\"eventData\":",
				this.eventDic.toJsonString(),
				"}"
			});
		}
	}
}
