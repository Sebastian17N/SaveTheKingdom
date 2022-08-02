using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TryAgainOrNextLevelButton : MonoBehaviour
{
    public TMP_Text ButtonText;
    FightSummaryGameManager FightSummaryGameManager;
    public string SceneName;
    public Button ChangingButton;

    void Start()
    {
        FightSummaryGameManager = FindObjectOfType<FightSummaryGameManager>();
        ChangingButton.onClick.AddListener(ChangeScene);
        if (FightSummaryGameManager.DidGamerWin == true)
        {
            ButtonText.text = "NEXT LEVEL";
        }
        else if (FightSummaryGameManager.DidGamerWin == false)
        {
            ButtonText.text = "TRY AGAIN";
        }
    }
    
    public void ChangeScene()
    {
        if (FightSummaryGameManager.DidGamerWin == true)
        {
            SceneManager.LoadScene(SceneName);
        }
        else if (FightSummaryGameManager.DidGamerWin == false)
        {
            SceneManager.LoadScene(SceneName);
        }
    }
              
}
