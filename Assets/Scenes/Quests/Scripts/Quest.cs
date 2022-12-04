using System;

namespace Assets.Scenes.Quests.Scripts
{
	[Serializable]
	public class Quest
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public QuestType Type { get; set; }
		public int Amount { get; set; }
	}
}
