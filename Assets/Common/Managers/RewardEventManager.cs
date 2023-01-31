using System;
using System.Collections.Generic;
using System.IO;
using Assets.Common.Enums;
using Assets.Common.JsonModel;
using Assets.Common.Models;
using UnityEngine;

namespace Assets.Common.Managers
{
	[Serializable]
	public class RewardEventManager
	{
		public List<CalendarRewardJsonModel> Rewards;
		public KingdomPassJsonModel KingdomPassRewards;

		public static RewardEventManager LoadCalendarRewardsManager(string filePath)
		{
			if (!File.Exists(filePath))
				return null;

			var fileData = File.ReadAllText(filePath);
			var prefs = JsonUtility.FromJson<RewardEventManager>(fileData);

			return prefs;
		}

		public static CalendarRewardJsonModel LoadCalendarRewards(string filePath)
		{
			if (!File.Exists(filePath))
				return null;

			var fileData = File.ReadAllText(filePath);
			var prefs = JsonUtility.FromJson<CalendarRewardJsonModel>(fileData);
			
			return prefs;
		}

		public static KingdomPassJsonModel LoadKingdomPassRewards(string filePath)
		{
			if (!File.Exists(filePath))
				return null;

			var fileData = File.ReadAllText(filePath);
			var prefs = JsonUtility.FromJson<KingdomPassJsonModel>(fileData);

			return prefs;
		}

		public static void Save(string filePath, RewardEventManager playerPreferences)
		{
			File.WriteAllText(filePath, JsonUtility.ToJson(playerPreferences));
		}
        public static void SaveCalendarRewards(string filePath, CalendarRewardJsonModel calendarRewardJsonModel)
        {
            File.WriteAllText(filePath, JsonUtility.ToJson(calendarRewardJsonModel));
        }

        public static void SaveKingdomPassReward(string filePath, KingdomPassJsonModel kingdomPassJsonModel)
        {
            File.WriteAllText(filePath, JsonUtility.ToJson(kingdomPassJsonModel));
        }
    }
}
