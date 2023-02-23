using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Common.JsonModel;
using Assets.Common.Models;
using Assets.Scenes.Quests.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestsManager : MonoBehaviour
{
    public GameObject QuestsPrefab;
    public GameObject ClaimButton;
    public Transform QuestsPrefabSpawnPoint;
    public Quest chosenQuest;
	public Image RewardImage;
    private readonly List<GameObject> _questsList = new();
    
    void Start()
    {
        SpawnDailyQuest();
        FillRewardIcon();
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
		    questObject.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = quest.Name;
		    questObject.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = quest.Description;
		    questObject.transform.Find("PointsRequireToEnd").GetComponent<TextMeshProUGUI>().text =
			    $"{(playerPreferences.PlayersAchievements.SingleOrDefault(achievement => achievement.OneDayQuest)?.AmountGathered ?? 0).ToString()} / {quest.RequiredAmountToEndQuest}"; // && achievement.QuestType == QuestType.DamageDealt
            questObject.GetComponent<QuestButton>().chosenQuest.RewardType = quest.RewardType;
            questObject.GetComponent<QuestButton>().chosenQuest.RewardAmount = quest.RewardAmount;

		    _questsList.Add(questObject);
	    }
    }
    public void FillRewardIcon()
    {
        RewardImage.sprite = AllIcons.GetIcon(_questsList[0].GetComponent<QuestButton>().chosenQuest.RewardType);
        RewardImage.GetComponentInChildren<TextMeshProUGUI>().text = _questsList[0].GetComponent<QuestButton>().chosenQuest.RewardAmount.ToString();
    }

    public void TakeQuestReward()
    {
        foreach (var quest in _questsList)
        {
            var singleQuest = quest.GetComponent<QuestButton>().chosenQuest;
            
            if (singleQuest == chosenQuest)
            {
                



            }
        }
    }
}
