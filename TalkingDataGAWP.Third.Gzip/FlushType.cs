using System;

namespace TalkingDataGAWP.Third.Gzip
{
	internal enum FlushType
	{
		None,
		Partial,
		Sync,
		Full,
		Finish
	}
}
