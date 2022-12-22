using System;

namespace Assets.Scenes.Quests.Scripts
{
	[Serializable]
	public class PlayerAchievement
	{
		public QuestType QuestType;
		public float AmountGathered;
		public bool OneDayQuest;
	}
}
