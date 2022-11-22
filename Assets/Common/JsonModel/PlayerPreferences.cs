using System;
using System.Collections.Generic;
using System.IO;
using Assets.Common.Enums;
using Assets.Common.Models;
using UnityEngine;

namespace Assets.Common.JsonModel
{
	[System.Serializable]
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
		public Gems EmeraldsJson = new();
		public Gems Emeralds
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
		public Gems SapphiresJson = new();
		public Gems Sapphires
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
		public Gems TopazesJson = new();
		public Gems Topazes
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
		public Gems MoonStoneJson = new();
		public Gems MoonStones
		{
			get => MoonStoneJson = Load().MoonStoneJson;
			set
			{
				MoonStoneJson = value;
				Save(this);
			}
		}

		public Gems AddGems
		{
			set
			{
				switch (value.TypeEnum)
				{
					case Enums.ResourcesTypeEnum.Emeralds:
						Emeralds.Amount += value.Amount;
						break;

					case Enums.ResourcesTypeEnum.Sapphires:
						Sapphires.Amount += value.Amount;
						break;

					case Enums.ResourcesTypeEnum.Topazes:
						Topazes.Amount += value.Amount;
						break;

					case Enums.ResourcesTypeEnum.MoonStones:
						MoonStones.Count += value.Count;
						break;
				}
			}
		}

		public List<Shards> Shards = new();

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
			Enum.TryParse(type, out ResourcesTypeEnum converted);
			return LoadResourceByType(converted);
		}

		public static int LoadResourceByType(ResourcesTypeEnum returnType)
		{
			switch (returnType)
			{
				case ResourcesTypeEnum.Coins:
					return Load().Coins;
				case ResourcesTypeEnum.Emeralds:
					return Load().Emeralds.Amount;
				case ResourcesTypeEnum.Sapphires:
					return Load().Sapphires.Amount;
				case ResourcesTypeEnum.Topazes:
					return Load().Topazes.Amount;
				case ResourcesTypeEnum.MoonStones:
					return Load().MoonStones.Amount;
			}

			return 0;
		}

		public static void SaveResourceByType(string type, int quantity)
		{
			Enum.TryParse(type, out ResourcesTypeEnum converted);
			SaveResourceByType(converted, quantity);
		}

		public static void SaveResourceByType(ResourcesTypeEnum type, int quantity)
		{
			var resource = Load();

			switch (type)
			{
				case ResourcesTypeEnum.Coins:
					resource.Coins += quantity;
					break;
				case ResourcesTypeEnum.Emeralds:
					resource.Emeralds.Amount += quantity;
					break;
				case ResourcesTypeEnum.Sapphires:
					resource.Sapphires.Amount += quantity;
					break;
				case ResourcesTypeEnum.Topazes:
					resource.Topazes.Amount += quantity;
					break;
				case ResourcesTypeEnum.MoonStones:
					resource.MoonStones.Amount += quantity;
					break;
			}
		}
	}
}