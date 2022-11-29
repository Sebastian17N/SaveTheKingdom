using System;
using System.Collections.Generic;

namespace Assets.Common.JsonModel
{
	public class KingdomPassJsonModel
	{
		public DateTime EventStartDateTime;
		public DateTime EventEndDateTime;

		public List<KingdomPassMilestoneJsonModel> Milestones;
	}
}
