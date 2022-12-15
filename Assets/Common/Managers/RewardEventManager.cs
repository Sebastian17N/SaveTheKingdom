using System;
using System.Collections.Generic;
using System.IO;
using Assets.Common.Enums;
using Assets.Common.Models;
using UnityEngine;

namespace Assets.Common.Managers
{
	[Serializable]
	public class RewardEventManager
	{
		public List<CalendarReward> Rewards;

		public static RewardEventManager LoadCalendarRewardsManager(string filePath)
		{
			if (!File.Exists(filePath))
				return null;

			var fileData = File.ReadAllText(filePath);
			var prefs = JsonUtility.FromJson<RewardEventManager>(fileData);

			return prefs;
		}

		public static List<CalendarReward> LoadCalendarRewards(string filePath)
		{
			if (!File.Exists(filePath))
				return null;

			var fileData = File.ReadAllText(filePath);
			var prefs = JsonUtility.FromJson<RewardEventManager>(fileData);
			
			return prefs?.Rewards;
		}

		public static void Save(string filePath, RewardEventManager playerPreferences)
		{
			File.WriteAllText(filePath, JsonUtility.ToJson(playerPreferences));
		}
	}
}
