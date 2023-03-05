using Assets.Common.Enums;
using Assets.Scenes.Quests.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    //public RevardState
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
    }
    private void Update()
    {
        WorkOnQuestButton();
    }

    private void WorkOnQuestButton()
    {
        if(chosenQuest.RewardState == RewardState.Active)
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
    }

    public void ShowQuestReward()
    {
        questsManager.chosenQuest = chosenQuest;
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