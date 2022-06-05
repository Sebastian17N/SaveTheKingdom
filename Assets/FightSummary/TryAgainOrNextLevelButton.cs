using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TryAgainOrNextLevelButton : MonoBehaviour
{
    public TMP_Text ButtonText;
    void Start()
    {
        //var fightSummaryGameManager = GetComponent<FightSummaryGameManager>();
        //if (fightSummaryGameManager.DidGamerWin == true)
        //{
        //}

        ButtonText.text = "NEXT LEVEL";

        
    }
        
}
