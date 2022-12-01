using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    public bool isQuestReadyToTake = false;
    public bool isQuestTaked = false;

    #region Prefab Elements
    [HideInInspector] public Image background;
    public TextMeshProUGUI questDescriptionsText;
    public TextMeshProUGUI questPointsRequireToEndText;
    #endregion
    void Start()
    {
        background = GetComponent<Image>();
    }
    private void Update()
    {
        QuestReadyToTake();
        QuestTaked();
    }
    private void QuestReadyToTake()
    {
        if (isQuestReadyToTake)
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
        if (isQuestTaked)
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
}