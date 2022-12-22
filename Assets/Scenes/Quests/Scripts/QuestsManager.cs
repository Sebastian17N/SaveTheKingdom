using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Common.JsonModel;
using Assets.Scenes.Quests.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestsManager : MonoBehaviour
{
    public GameObject questsPrefab;
    public Transform questsPrefabSpawnPoint;
    public GameObject rewardIcon;
    private List<GameObject> questsList = new List<GameObject>();
    public List<QuestScriptableObject> questScriptableObjectList = new List<QuestScriptableObject>();
    void Start()
    {
        SpawnQuest(QuestOrigin.Daily);
    }

    void Update()
    {
        
    }

    public void SpawnDailyQuest()
    {
        foreach (var quest in questsList)
        {
            Destroy(quest);
        }

        SpawnQuest(QuestOrigin.Daily);
    }

    public void SpawnGeneralQuest()
    {
        foreach (var quest in questsList)
        {
            Destroy(quest);
        }

        SpawnQuest(QuestOrigin.General);
    }

    private void SpawnQuest(QuestOrigin questOrigin)
    {
        var questForOrigin = questScriptableObjectList.Where(quest => quest.origin == questOrigin).ToList();
        //for(int i = 0; i < questForOrigin.Count; i++)
        //{
        //    CreateQuests(questForOrigin[i]);
        //}
        CreateQuests1();
    }

    private void CreateQuests(QuestScriptableObject questScriptableObject)
    {
        var quest = Instantiate(questsPrefab, questsPrefabSpawnPoint);
        quest.transform.Find("QuestDescriptionsText").GetComponent<TextMeshProUGUI>().text =
            questScriptableObject.questDescriptions;
        quest.transform.Find("QuestPointsRequireToEndText").GetComponent<TextMeshProUGUI>().text =
            $"{questScriptableObject.currentPointsRequireToEndQuest} / " +
            $"{questScriptableObject.totalPointsRequireToEndQuest}";

        questsList.Add(quest);
    }

    private void CreateQuests1()
    {
	    var fileData = File.ReadAllText("Assets/Scenes/Quests/Samples/DamageDealt_1.json");
	    var quest = JsonUtility.FromJson<Quest>(fileData);

	    var playerPreferences = PlayerPreferences.Load();

        var questObject = Instantiate(questsPrefab, questsPrefabSpawnPoint);
        questObject.transform.Find("QuestDescriptionsText").GetComponent<TextMeshProUGUI>().text =
	        quest.Name;
        questObject.transform.Find("QuestPointsRequireToEndText").GetComponent<TextMeshProUGUI>().text =
	        $"{(playerPreferences.PlayersAchievements.SingleOrDefault(achievement => achievement.OneDayQuest && achievement.QuestType == QuestType.DamageDealt)?.AmountGathered ?? 0).ToString()} / {quest.RequiredAmountToEndQuest}";

	    questsList.Add(questObject);
    }
}
