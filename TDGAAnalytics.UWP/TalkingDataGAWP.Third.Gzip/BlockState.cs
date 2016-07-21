using System;

namespace TalkingDataGAWP.Third.Gzip
{
	internal enum BlockState
	{
		NeedMore,
		BlockDone,
		FinishStarted,
		FinishDone
	}
}
