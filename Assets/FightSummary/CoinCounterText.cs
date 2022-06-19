using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounterText : MonoBehaviour
{
    public int CoinsNumber;
    void Start()
    {
        RefreshText();        
    }

    void Update()
    {
        IncrementCoins();
    }
    
    public  void IncrementCoins()
    {
        
        RefreshText();
        ReadCoinsNumber();
    }
    void RefreshText()
    {
        GetComponent<TMP_Text>().text = CoinsNumber.ToString();
        ReadCoinsNumber();
    }
    void ReadCoinsNumber()
    {
        PlayerPrefs.GetInt("coins");
    }
}
