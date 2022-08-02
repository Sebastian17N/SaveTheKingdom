using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounterText : MonoBehaviour
{
    public int CoinsNumber;
    void Start()
    {
        CoinsNumber = PlayerPrefs.GetInt("coins");
        GetComponent<TMP_Text>().text = CoinsNumber.ToString();
    }
    
    public  void IncrementCoins(int coinsToAdd)
    {
        CoinsNumber = PlayerPrefs.GetInt("coins") + coinsToAdd;
        PlayerPrefs.SetInt("coins", CoinsNumber);
        GetComponent<TMP_Text>().text = CoinsNumber.ToString();        
    }
    
}
