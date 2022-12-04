using System;
using Assets.Common.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Assets.Common.JsonModel
{
	[Serializable]
	public class MapsConfigJsonModel
	{
		public int Id;
		public string SpriteBackgroundPath;
		public string EnemiesMapFileName;
		public int[] AvailableUnitsToPlayById;
		public int[] AwardCoins;
		public RewardType AwardGemsType;
		public int[] AwardGemsNumber;
		public AwardShardJsonModel[] AwardShards;
	}
}
