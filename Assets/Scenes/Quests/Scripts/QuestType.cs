using System;

namespace Assets.Scenes.Quests.Scripts
{
	[Serializable]
	public enum QuestType
	{
		DamageDealt,
		ChestOpened,
        CampaignMissionsCompleted,
        StoreShopping,
		UnitTrained,
		UnitLevelReached,
		UnitsUnloced,
		MapUncloced,
		EnemiesKilled,
		LogInEveryDay,
        QuestFinished
    }
}