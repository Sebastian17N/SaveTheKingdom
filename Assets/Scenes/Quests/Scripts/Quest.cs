using System;
using Assets.Common.Enums;

namespace Assets.Scenes.Quests.Scripts
{
	[Serializable]
	public class Quest
	{
		public string FileName;
		public string Name;
		public string Description;
		public int RequiredAmountToEndQuest;
		public QuestType QuestType;
		public RewardType RewardType;
        public RewardState RewardState;
        public int RewardAmount;
	}
}