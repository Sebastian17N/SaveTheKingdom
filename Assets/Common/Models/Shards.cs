using System;

namespace Assets.Common.Models
{
	[Serializable]
	public class Shards
	{
		public int ShardId;
		public int Amount;

		public Shards(int shardId, int amount)
		{
			ShardId = shardId;
			Amount = amount;
		}
	}
}
