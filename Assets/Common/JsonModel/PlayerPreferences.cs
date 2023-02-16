using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Common.Enums;
using Assets.Common.Models;
using Assets.Scenes.Quests.Scripts;
using UnityEngine;

namespace Assets.Common.JsonModel
{
	[Serializable]
	public class PlayerPreferences
	{
		/// <summary>
		/// DO NOT USE IT DIRECTLY
		/// </summary>
		public int CoinsJson;
		public int Coins
		{
			get => CoinsJson = Load().CoinsJson;
			set
			{
				CoinsJson = value;
				Save(this);
			}
		}

		/// <summary>
		/// DO NOT USE IT DIRECTLY
		/// </summary>
		public Reward EmeraldsJson = new();
		public Reward Emeralds
		{
			get => EmeraldsJson = Load().EmeraldsJson;
			set
			{
				EmeraldsJson = value;
				Save(this);
			}
		}

		/// <summary>
		/// DO NOT USE IT DIRECTLY
		/// </summary>
		public Reward SapphiresJson = new();
		public Reward Sapphires
		{
			get => SapphiresJson = Load().SapphiresJson;
			set
			{
				SapphiresJson = value;
				Save(this);
			}
		}

		/// <summary>
		/// DO NOT USE IT DIRECTLY
		/// </summary>
		public Reward TopazesJson = new();
		public Reward Topazes
		{
			get => TopazesJson = Load().TopazesJson;
			set
			{
				TopazesJson = value;
				Save(this);
			}
		}

		/// <summary>
		/// DO NOT USE IT DIRECTLY
		/// </summary>
		public Reward MoonStoneJson = new();
		public Reward MoonStones
		{
			get => MoonStoneJson = Load().MoonStoneJson;
			set
			{
				MoonStoneJson = value;
				Save(this);
			}
		}

		public Reward AddReward
		{
			set
			{
				switch (value.Type)
				{
					case RewardType.Coins:
						Coins += value.Amount;
						break;
					case RewardType.Emeralds:
						var emeralds = Emeralds;
						emeralds.Amount += value.Amount;
						emeralds.Type = RewardType.Emeralds;
						break;

					case RewardType.Sapphires:
						var sapphires = Sapphires;
						sapphires.Amount += value.Amount;
						sapphires.Type = RewardType.Sapphires;
						break;

					case RewardType.Topazes:
						var topazes = Topazes;
						topazes.Amount += value.Amount;
						topazes.Type = RewardType.Topazes;
						break;

					case RewardType.MoonStones:
						var moonStones = MoonStones;
						moonStones.Amount += value.Amount;
						moonStones.Type = RewardType.MoonStones;
						break;
				}

				Save(this);
			}
		}

		/// <summary>
		/// DO NOT USE IT DIRECTLY
		/// </summary>
		public List<Shards> ShardsJson = new();
		public List<Shards> Shards
		{
			get => ShardsJson = Load().ShardsJson;
			set
			{
				ShardsJson = value;
				Save(this);
			}
		}

		public Shards AddShards
		{
			set
			{
				if (Shards.Any(shard => shard.ShardId == value.ShardId))
					Shards.Single(shard => shard.ShardId == value.ShardId).Amount += value.Amount;
				else
				{
					var shardsCopy = Shards.ToList();
					shardsCopy.Add(new Shards (value.ShardId, value.Amount));
					Shards = shardsCopy.ToList();
				}

				Save(this);
			}
		}

		private DateTime _oneDateQuest;
		private void RefreshOneDateQuest()
		{
			if (_oneDateQuest.Date.Equals(DateTime.Today)) 
					return;

			foreach (var achievement in PlayersAchievements.Where(achievement => achievement.OneDayQuest))
			{
				achievement.AmountGathered = 0;
			}
			_oneDateQuest = DateTime.Today;
		}
		
		public List<PlayerAchievement> PlayersAchievements = new();
		
		private static readonly string fileName = "Assets/Configuration/PlayerPreferences.json";

		public static PlayerPreferences Load()
		{
			if (!File.Exists(fileName))
				return null;

			var fileData = File.ReadAllText(fileName);
			var prefs = JsonUtility.FromJson<PlayerPreferences>(fileData) ?? new PlayerPreferences();
			return prefs;
		}

		private static void Save(PlayerPreferences playerPreferences)
		{
			File.WriteAllText(fileName, JsonUtility.ToJson(playerPreferences));
		}

		public static int LoadResourceByType(string type)
		{
			Enum.TryParse(type, out RewardType converted);
			return LoadResourceByType(converted);
		}

		public static int LoadResourceByType(RewardType returnType)
		{
			switch (returnType)
			{
				case RewardType.Coins:
					return Load().Coins;
				case RewardType.Emeralds:
					return Load().Emeralds.Amount;
				case RewardType.Sapphires:
					return Load().Sapphires.Amount;
				case RewardType.Topazes:
					return Load().Topazes.Amount;
				case RewardType.MoonStones:
					return Load().MoonStones.Amount;
			}

			return 0;
		}

		/// <summary>
		/// Method logs damage dealt (for example during one battle) for achievement's system.
		/// </summary>
		/// <param name="damageDealt">Damage dealt.</param>
		public static void LogHowMuchDamageWasDealtForTheAchievements(float damageDealt)
		{
			var playerPreferences = Load();
			var achievements = playerPreferences.PlayersAchievements.Where(achievement => achievement.QuestType == QuestType.DamageDealt);

			playerPreferences.RefreshOneDateQuest();

			var oneDayAchievement = achievements.SingleOrDefault(achievement => achievement.OneDayQuest);
			var permanentAchievement = achievements.SingleOrDefault(achievement => !achievement.OneDayQuest);

			if (oneDayAchievement != null)
				oneDayAchievement.AmountGathered += damageDealt;
			else
				playerPreferences
					.PlayersAchievements
					.Add(new PlayerAchievement
					{
						AmountGathered = damageDealt,
						OneDayQuest = true,
						QuestType = QuestType.DamageDealt
					});

			if (permanentAchievement != null)
				permanentAchievement.AmountGathered += damageDealt;
			else
				playerPreferences
					.PlayersAchievements
					.Add(new PlayerAchievement
					{
						AmountGathered = damageDealt,
						OneDayQuest = false,
						QuestType = QuestType.DamageDealt
					});
			Save(playerPreferences);
		}
	}
}