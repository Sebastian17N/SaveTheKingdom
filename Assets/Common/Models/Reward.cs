using System;
using Assets.Common.Enums;

namespace Assets.Common.Models
{
	[Serializable]
	public class Reward
	{
		public int Amount;
		public RewardType Type;
		public RewardState State;
	}
}