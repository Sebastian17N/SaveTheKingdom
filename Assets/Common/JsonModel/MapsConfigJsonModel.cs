using System;

namespace Assets.Common.JsonModel
{
	[Serializable]
	public class MapsConfigJsonModel
	{
		public int Id;
		public int StarRatingNeededToStartLevel;
		public string SpriteBackgroundPath;
		//public string TerrainMapPath;
		public string EnemiesMapFileName;
		public int [] AwardCoins;
		public int[] AwardGems;
		public int[] AwardShards;
	}
}
