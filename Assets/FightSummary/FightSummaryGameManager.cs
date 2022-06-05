using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightSummaryGameManager : MonoBehaviour
{
    public bool DidGamerWin = false;
    public GameObject WinLoseImage;
    public Sprite[] WinLoseImages;
    
    void Start()
    {
        ShowWinLoseImage();
    }

    void Update()
    {
        
    }
    private void ShowWinLoseImage()
    {        
        if(DidGamerWin)
        {
            WinLoseImage.gameObject.GetComponent<SpriteRenderer>().sprite = WinLoseImages[0];
        }
        else
        {
            WinLoseImage.gameObject.GetComponent<SpriteRenderer>().sprite = WinLoseImages[1];
            WinLoseImage.transform.position = new Vector3( 0, 0.6f, 0);
        }
    }
}
