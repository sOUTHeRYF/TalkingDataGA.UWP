using System;
using System.Runtime.Serialization;

namespace TalkingDataGAWP.Entity
{
	[DataContract]
	internal class EventInit : EventBase
	{
		public EventInit() : base("G2")
		{
		}

		protected override void initializeEventCustomizeMap()
		{
		}
	}
}
