using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Common.Enums;
using Assets.Common.JsonModel;
using Assets.Common.Models;
using Assets.Scenes.Quests.Scripts;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class QuestsManager : MonoBehaviour
{
    public GameObject QuestsPrefab;
    public GameObject ClaimButton;
    public Transform QuestsPrefabSpawnPoint;
    public Quest ChosenQuest;
    public Image RewardImage;
    public List<GameObject> QuestsList = new();
    //public PlayerPreferences PlayerPreferences;

    void Start()
    {
        var preferences = PlayerPreferences.Load();
        SpawnDailyQuest();
        FillRewardIcon();
        preferences.RefreshOneDateQuest();
        //PlayerPreferences.CheckIfAllDailyQuestHaveBenTaked();
    }
    private void Update()
    {
        RefreshQuestData();
    }
    public void SpawnDailyQuest()
    {
        SpawnQuests(QuestOrigin.Daily);
    }

    public void SpawnGeneralQuest()
    {
        SpawnQuests(QuestOrigin.General);
    }

    public void SpawnQuests(QuestOrigin questOrigin)
    {
        foreach (var quest in QuestsList)
        {
            Destroy(quest);
        }
        QuestsList.Clear();

        var directoryInfo = new DirectoryInfo($"Assets/Scenes/Quests/Data/{(questOrigin == QuestOrigin.General ? "General" : "Daily")}");
        var files = directoryInfo.GetFiles("*.json");

        foreach (var file in files)
        {
            var fileData = File.ReadAllText(file.FullName);
            var quest = JsonUtility.FromJson<Quest>(fileData);
            var playerPreferences = PlayerPreferences.Load();

            var questObject = Instantiate(QuestsPrefab, QuestsPrefabSpawnPoint);
            questObject.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = quest.Name;
            questObject.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = quest.Description;

            var aaa = playerPreferences.PlayersAchievements.SingleOrDefault(achievement => (achievement.OneDayQuest && questOrigin == QuestOrigin.Daily || !achievement.OneDayQuest && questOrigin == QuestOrigin.General) && achievement.QuestType == quest.QuestType)?.AmountGathered ?? 0;

            questObject.transform.Find("PointsRequireToEnd").GetComponent<TextMeshProUGUI>().text =
                $"{(aaa > quest.RequiredAmountToEndQuest? quest.RequiredAmountToEndQuest : aaa).ToString()} / {quest.RequiredAmountToEndQuest}";
            questObject.GetComponent<QuestButton>().chosenQuest = quest;
            questObject.GetComponent<QuestButton>().chosenQuest.FileName = file.FullName;
           
            QuestsList.Add(questObject);
        }
    }
    public void RefreshQuestData()
    {
        //var directoryInfo = new DirectoryInfo($"Assets/Scenes/Quests/Data/{(QuestOrigin.General ? "General" : "Daily")}");
        //var files = directoryInfo.GetFiles("*.json");

        //foreach (var file in QuestsList)
        //{
        //    var fileData = File.ReadAllText(file.FullName);

        //    var quest = JsonUtility.FromJson<Quest>(fileData);
        //    var playerPreferences = PlayerPreferences.Load();

        //    var questObject = Instantiate(QuestsPrefab, QuestsPrefabSpawnPoint);
        //    questObject.transform.Find("PointsRequireToEnd").GetComponent<TextMeshProUGUI>().text =
        //        $"{(playerPreferences.PlayersAchievements.SingleOrDefault(achievement => achievement.OneDayQuest && achievement.QuestType == quest.QuestType)?.AmountGathered ?? 0).ToString()} / {quest.RequiredAmountToEndQuest}";
        //    questObject.GetComponent<QuestButton>().chosenQuest = quest;
        //    questObject.GetComponent<QuestButton>().chosenQuest.FileName = file.FullName;

        //    QuestsList.Add(questObject);
        //}
    }
    public void FillRewardIcon()
    {
        RewardImage.sprite = AllIcons.GetIcon(QuestsList[0].GetComponent<QuestButton>().chosenQuest.RewardType);
        RewardImage.GetComponentInChildren<TextMeshProUGUI>().text = QuestsList[0].GetComponent<QuestButton>().chosenQuest.RewardAmount.ToString();
    }

    public void TakeQuestReward()
    {
        foreach (var quest in QuestsList)
        {
            var singleQuest = quest.GetComponent<QuestButton>().chosenQuest;

            if (singleQuest == ChosenQuest && ChosenQuest.RewardState == RewardState.Active)
            {

                ResourcesMasterController.AddAndUpdateResources(singleQuest.RewardType, singleQuest.RewardAmount);

                singleQuest.RewardState = RewardState.Taken;

                ClaimButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.60f);

                SaveQuest(singleQuest);

                PlayerPreferences.LogGatherAchievements(1, QuestType.QuestFinished6);

                break;
            }
        }
    }
    public static void SaveQuest(Quest quest)
    {
        File.WriteAllText(quest.FileName, JsonUtility.ToJson(quest));
    }
}
