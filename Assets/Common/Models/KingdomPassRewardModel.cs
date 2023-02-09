using System;

namespace Assets.Common.Models
{
	[Serializable]
	public class KingdomPassRewardModel
	{
		public int Level;
		public Reward RegularReward;
		public Reward PremiumReward;
	}
}
