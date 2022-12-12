using System;

namespace Assets.Common.Models
{
	[Serializable]
	public class KingdomPassReward
	{
		public int Level;
		public Reward RegularReward;
		public Reward PremiumReward;
	}
}
