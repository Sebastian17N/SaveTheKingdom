using System;
using Assets.Common.Enums;

namespace Assets.Scenes.Quests.Scripts
{
	[Serializable]
	public class Quest
	{
		public string Name;
		public string Description;
		public int RequiredAmountToEndQuest;
		public QuestType Type;
		public RewardType RewardType;
		public int RewardAmount;
	}
}