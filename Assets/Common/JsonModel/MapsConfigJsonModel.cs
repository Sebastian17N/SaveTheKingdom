using System;
using Assets.Common.Enums;

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
		public Enums.ResourcesTypeEnum AwardResourcesesTypeEnum;
		public int[] AwardGemsNumber;
		public AwardShardJsonModel[] AwardShards;
	}
}
