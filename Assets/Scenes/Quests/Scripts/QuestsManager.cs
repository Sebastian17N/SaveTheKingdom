using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        for(int i = 0; i < questForOrigin.Count; i++)
        {
            CreateQuests(questForOrigin[i]);
        }
    }
    private void CreateQuests(QuestScriptableObject questScriptableObject)
    {
        var quest = Instantiate(questsPrefab, questsPrefabSpawnPoint);
        quest.transform.Find("QuestDescriptionsText").GetComponent<TextMeshProUGUI>().text =
            questScriptableObject.questDescriptions.ToString();
        quest.transform.Find("QuestPointsRequireToEndText").GetComponent<TextMeshProUGUI>().text =
            $"{questScriptableObject.currentPointsRequireToEndQuest.ToString()} / " +
            $"{questScriptableObject.totalPointsRequireToEndQuest.ToString()}";

        questsList.Add(quest);
    }
}
