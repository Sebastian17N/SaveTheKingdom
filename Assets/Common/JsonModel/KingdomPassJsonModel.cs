using Assets.Common.Models;
using System;
using System.Collections.Generic;

namespace Assets.Common.JsonModel
{
	[Serializable]
	public class KingdomPassJsonModel
	{
		public string EventName;
		public string EventStartDateTime;
		public string EventEndDateTime;
		public List<KingdomPassRewardModel> KingdomPassRewards;
	}
}
