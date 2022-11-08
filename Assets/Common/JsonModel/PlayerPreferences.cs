using System;
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

		public Gems AddGems
		{
			set
			{
				switch (value.TypeEnum)
				{
					case Enums.ResourcesTypeEnum.Emeralds:
						Emeralds.Count += value.Count;
						break;

					case Enums.ResourcesTypeEnum.Sapphires:
						Sapphires.Count += value.Count;
						break;

					case Enums.ResourcesTypeEnum.Topazes:
						Topazes.Count += value.Count;
						break;
				}
			}
		}

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
					return Load().Emeralds.Count;
				case ResourcesTypeEnum.Sapphires:
					return Load().Sapphires.Count;
				case ResourcesTypeEnum.Topazes:
					return Load().Topazes.Count;
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
					resource.Emeralds.Count += quantity;
					break;
				case ResourcesTypeEnum.Sapphires:
					resource.Sapphires.Count += quantity;
					break;
				case ResourcesTypeEnum.Topazes:
					resource.Topazes.Count += quantity;
					break;
			}
		}
	}
}