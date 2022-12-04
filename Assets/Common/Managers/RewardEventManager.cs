using System;
using System.Collections.Generic;
using System.IO;
using Assets.Common.Models;
using UnityEngine;

namespace Assets.Common.Managers
{
	[Serializable]
	public class RewardEventManager
	{
		public List<Reward> Rewards;
		
		private static readonly string fileName = "Assets/Configuration/RewardEventManager_2022_12.json";
		public static RewardEventManager Load()
		{
			if (!File.Exists(fileName))
				return null;

			var fileData = File.ReadAllText(fileName);
			var prefs = JsonUtility.FromJson<RewardEventManager>(fileData) ?? new RewardEventManager();
			return prefs;
		}
	}
}
