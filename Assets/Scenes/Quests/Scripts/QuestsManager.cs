using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Common.JsonModel;
using Assets.Scenes.Quests.Scripts;
using TMPro;
using UnityEngine;

public class QuestsManager : MonoBehaviour
{
    public GameObject QuestsPrefab;
    public Transform QuestsPrefabSpawnPoint;
    private readonly List<GameObject> _questsList = new();
    
    void Start()
    {
	    SpawnGeneralQuest();
    }

    public void SpawnDailyQuest()
    {
        SpawnQuests(QuestOrigin.Daily);
    }

    public void SpawnGeneralQuest()
    {
	    SpawnQuests(QuestOrigin.General);
    }

    public void SpawnQuests(QuestOrigin questType)
    {
	    foreach (var quest in _questsList)
	    {
		    Destroy(quest);
	    }

        var directoryInfo = new DirectoryInfo($"Assets/Scenes/Quests/Data/{(questType == QuestOrigin.General ? "General" : "Daily")}");
	    var files = directoryInfo.GetFiles("*.json");

	    foreach (var file in files)
	    {
		    var fileData = File.ReadAllText(file.FullName);
            
		    var quest = JsonUtility.FromJson<Quest>(fileData);
		    var playerPreferences = PlayerPreferences.Load();

		    var questObject = Instantiate(QuestsPrefab, QuestsPrefabSpawnPoint);
		    questObject.transform.Find("Name").GetComponent<TextMeshProUGUI>().text =
			    quest.Name;
		    questObject.transform.Find("Description").GetComponent<TextMeshProUGUI>().text =
			    quest.Description;
		    questObject.transform.Find("PointsRequireToEnd").GetComponent<TextMeshProUGUI>().text =
			    $"{(playerPreferences.PlayersAchievements.SingleOrDefault(achievement => achievement.OneDayQuest && achievement.QuestType == QuestType.DamageDealt)?.AmountGathered ?? 0).ToString()} / {quest.RequiredAmountToEndQuest}";

		    _questsList.Add(questObject);
	    }
    }
}
