using System;
using System.Runtime.Serialization;

namespace Assets.Common.JsonModel
{
	[Serializable]
	public class AwardShardJsonModel
	{
		public int UnitId;
		[OptionalField]
		public bool FirstWin = false;
		public int[] MinRange;
		public int[] MaxRange;
	}
}
