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
						Emeralds.Amount += value.Amount;
						Emeralds.Type = RewardType.Emeralds;
						break;

					case RewardType.Sapphires:
						Sapphires.Amount += value.Amount;
						Sapphires.Type = RewardType.Sapphires;
						break;

					case RewardType.Topazes:
						Topazes.Amount += value.Amount;
						Topazes.Type = RewardType.Topazes;
						break;

					case RewardType.MoonStones:
						MoonStones.Amount += value.Amount;
						MoonStoneJson.Type = RewardType.MoonStones;
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
		public DateTime OneDayQuest
		{
			get
			{
				if (!_oneDateQuest.Date.Equals(DateTime.Today))
					_oneDateQuest = DateTime.Today;

				return _oneDateQuest;
			}
		}

		// Typ questa, ile zebrane danego typu, czy jednodniowy
		private List<(QuestType, int, bool)> ZebraneAchivmentyUGracza { get; set; }
		

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

		//public static void SaveResourceByType(string type, int quantity)
		//{
		//	Enum.TryParse(type, out RewardType converted);
		//	SaveResourceByType(converted, quantity);
		//}

		//public static void SaveResourceByType(RewardType type, int quantity)
		//{
		//	var resource = Load();

		//	switch (type)
		//	{
		//		case RewardType.Coins:
		//			resource.Coins += quantity;
		//			break;
		//		case RewardType.Emeralds:
		//			resource.Emeralds.Amount += quantity;
		//			break;
		//		case RewardType.Sapphires:
		//			resource.Sapphires.Amount += quantity;
		//			break;
		//		case RewardType.Topazes:
		//			resource.Topazes.Amount += quantity;
		//			break;
		//		case RewardType.MoonStones:
		//			resource.MoonStones.Amount += quantity;
		//			break;
		//	}
		//}
	}
}