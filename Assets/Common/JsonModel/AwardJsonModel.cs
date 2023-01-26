using System;
using System.Runtime.Serialization;
using Assets.Common.Enums;

namespace Assets.Common.JsonModel
{
	[Serializable]
	public class AwardJsonModel
	{
		public RewardType Type;
		public int[] Amount;
	}
}
