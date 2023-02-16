using System;
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
