using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResouresCounters : MonoBehaviour
{
    public int CoinsNumber;
    public int SapphireNumber;
    public int TopazNumber;
    public int EmeraldNumber;
    public int MoonStoneNumber;
    void Start()
    {
        //CoinsNumber = 100000;
        //SapphireNumber = 10000;
        //TopazNumber = 20000;
        //EmeraldNumber = 30000;
        //MoonStoneNumber = 40000;

        AssignmentResoures();
        ShowResoures();
    }

    void Update()
    {
        
    }
    private void AssignmentResoures()
    {
        //PlayerPrefs.SetInt("coins", CoinsNumber);
        //PlayerPrefs.SetInt("sapphire", SapphireNumber);
        //PlayerPrefs.SetInt("topaz", TopazNumber);
        //PlayerPrefs.SetInt("emerald", EmeraldNumber);
        //PlayerPrefs.SetInt("moonStone", MoonStoneNumber);

        CoinsNumber = PlayerPrefs.GetInt("coins");
        SapphireNumber = PlayerPrefs.GetInt("sapphire");
        TopazNumber = PlayerPrefs.GetInt("topaz");
        EmeraldNumber = PlayerPrefs.GetInt("emerald");
        MoonStoneNumber = PlayerPrefs.GetInt("moonStone");

    }
    private void ShowResoures()
    {
        transform.Find("CoinCounter/CoinsNumberText").GetComponent<TMP_Text>().text = CoinsNumber.ToString();
        transform.Find("SapphireCounter/SapphireNumberText").GetComponent<TMP_Text>().text = SapphireNumber.ToString();
        transform.Find("TopazCounter/TopazNumberText").GetComponent<TMP_Text>().text = TopazNumber.ToString();
        transform.Find("EmeraldCounter/EmeraldNumberText").GetComponent<TMP_Text>().text = EmeraldNumber.ToString();
        transform.Find("MoonStoneCounter/MoonStoneNumberText").GetComponent<TMP_Text>().text = MoonStoneNumber.ToString();
    }
    public void IncrementResoures(int resouresToAdd, string resouresType)
    {
        switch (resouresType)
        {
            case "coins":
                CoinsNumber = PlayerPrefs.GetInt("coins") + resouresToAdd;
                PlayerPrefs.SetInt("coins", CoinsNumber);
                transform.Find("CoinCounter/CoinsNumberText").GetComponent<TMP_Text>().text = CoinsNumber.ToString();
                break;
            case "sapphire":
                SapphireNumber = PlayerPrefs.GetInt("sapphire") + resouresToAdd;
                PlayerPrefs.SetInt("sapphire", SapphireNumber);
                transform.Find("SapphireCounter/SapphireNumberText").GetComponent<TMP_Text>().text = SapphireNumber.ToString();
                break;
            case "topaz":
                TopazNumber = PlayerPrefs.GetInt("topaz") + resouresToAdd;
                PlayerPrefs.SetInt("topaz", TopazNumber);
                transform.Find("TopazCounter/TopazNumberText").GetComponent<TMP_Text>().text = TopazNumber.ToString();
                break;
            case "emerald":
                EmeraldNumber = PlayerPrefs.GetInt("emerald") + resouresToAdd;
                PlayerPrefs.SetInt("emerald", EmeraldNumber);
                transform.Find("EmeraldCounter/EmeraldNumberText").GetComponent<TMP_Text>().text = EmeraldNumber.ToString();
                break;
            case "moonStone":
                MoonStoneNumber = PlayerPrefs.GetInt("moonStone") + resouresToAdd;
                PlayerPrefs.SetInt("moonStone", MoonStoneNumber);
                transform.Find("MoonStoneCounter/MoonStoneNumberText").GetComponent<TMP_Text>().text = MoonStoneNumber.ToString();
                break;

            default:
                break;
        }
    }
    public void DecrementResoures(int resouresToRemove, string resouresType)
    {
        CoinsNumber = PlayerPrefs.GetInt("coins") - resouresToRemove;
        PlayerPrefs.SetInt("coins", CoinsNumber);
        GetComponent<TMP_Text>().text = CoinsNumber.ToString();
    }
}
