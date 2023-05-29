using Assets.Common.Enums;
using Assets.Common.JsonModel;
using Assets.Scenes.Quests.Scripts;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    public Quest chosenQuest;
    public QuestsManager questsManager;

    #region Prefab Elements
    [HideInInspector] public Image background;
    public TextMeshProUGUI questDescriptionsText;
    public TextMeshProUGUI questNameText;
    public TextMeshProUGUI questPointsRequireToEndText;
    #endregion
    void Start()
    {
        background = GetComponent<Image>();
        questsManager = FindObjectOfType<QuestsManager>();
        WorkOnQuestButton();
    }
    private void Update()
    {
        WorkOnQuestButton();
    }

    private void WorkOnQuestButton()
    {
        var playerPreferences = PlayerPreferences.Load();


        if (playerPreferences.PlayersAchievements.SingleOrDefault(achievement => achievement.QuestType == chosenQuest.QuestType && achievement.OneDayQuest && chosenQuest.RewardState != RewardState.Taken)?.AmountGathered >= chosenQuest.RequiredAmountToEndQuest)
        {
            chosenQuest.RewardState = RewardState.Active;
        }

        if (chosenQuest.RewardState == RewardState.Active)
        {
            background.color = Color.white;
            questPointsRequireToEndText.color = Color.black;
            questDescriptionsText.color = Color.black;
            questNameText.color = Color.black;
            
        }
        else if(chosenQuest.RewardState == RewardState.Inactive)
        {
            background.color = new Color(0.65f, 0.65f, 0.65f, 0.60f);
            questPointsRequireToEndText.color = Color.white;
            questDescriptionsText.color = Color.white;
            questNameText.color = Color.white;
        }
        else if(chosenQuest.RewardState == RewardState.Taken)
        {
            background.color = new Color(0.65f, 0.65f, 0.65f, 0.60f);
            questPointsRequireToEndText.color = Color.black;
            questDescriptionsText.color = Color.black;
            questNameText.color = Color.black;
        }

        if (playerPreferences.PlayersAchievements.SingleOrDefault(achievement => achievement.QuestType == chosenQuest.QuestType && achievement.OneDayQuest && chosenQuest.RewardState == RewardState.Taken)?.AmountGathered < chosenQuest.RequiredAmountToEndQuest)
        {
            chosenQuest.RewardState = RewardState.Inactive;
        }
    }

    public void ShowQuestReward()
    {
        questsManager.ChosenQuest = chosenQuest;
        questsManager.RewardImage.sprite = AllIcons.GetIcon(chosenQuest.RewardType);
        questsManager.RewardImage.GetComponentInChildren<TextMeshProUGUI>().text = chosenQuest.RewardAmount.ToString();


        if (chosenQuest.RewardState == RewardState.Active)
        {
            questsManager.ClaimButton.GetComponent<Image>().color = new Color(0.32f, 1f, 0f, 1f);
        }
        else
        {
            questsManager.ClaimButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.60f);
        }
    }

}