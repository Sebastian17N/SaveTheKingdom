using System;
using Assets.Common.Enums;

namespace Assets.Scenes.Quests.Scripts
{
	[Serializable]
	public class Quest
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public QuestType Type { get; set; }
		public int RequiredAmountToEndQuest { get; set; }
		public RewardType RewardType { get; set; }
		public int RewardAmount { get; set; }
	}
}