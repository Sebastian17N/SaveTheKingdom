using Assets.Common.Enums;
using Assets.Scenes.Quests.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    //public bool isQuestReadyToTake = false;
    //public bool isQuestTaked = false;
    //public RevardState
    public Quest chosenQuest;
    public QuestsManager questsManager;

    #region Prefab Elements
    [HideInInspector] public Image background;
    public TextMeshProUGUI questDescriptionsText;
    public TextMeshProUGUI questPointsRequireToEndText;
    #endregion
    void Start()
    {
        background = GetComponent<Image>();
        questsManager = FindObjectOfType<QuestsManager>();
    }
    private void Update()
    {
        QuestReadyToTake();
        QuestTaked();
    }
    private void QuestReadyToTake()
    {
        if (chosenQuest.RewardState == RewardState.Active)
        {
            background.color = Color.white;
        }
        else
        {
            background.color = new Color(0.65f, 0.65f, 0.65f, 0.60f);
        }
    }
    private void QuestTaked()
    {
        if (chosenQuest.RewardState == RewardState.Taken)
        {
            questDescriptionsText.color = Color.black;
            questPointsRequireToEndText.color = Color.black;
        }
        else
        {
            questDescriptionsText.color = Color.white;
            questPointsRequireToEndText.color = Color.white;
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