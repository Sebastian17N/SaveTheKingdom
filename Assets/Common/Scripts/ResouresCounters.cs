using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcesCounters : MonoBehaviour
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

        AssignmentResources();
        ShowResources();
    }

    void Update()
    {
        
    }
    private void AssignmentResources()
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
    private void ShowResources()
    {
        transform.Find("CoinCounter/CoinsNumberText").GetComponent<TMP_Text>().text = CoinsNumber.ToString();
        transform.Find("SapphireCounter/SapphireNumberText").GetComponent<TMP_Text>().text = SapphireNumber.ToString();
        transform.Find("TopazCounter/TopazNumberText").GetComponent<TMP_Text>().text = TopazNumber.ToString();
        transform.Find("EmeraldCounter/EmeraldNumberText").GetComponent<TMP_Text>().text = EmeraldNumber.ToString();
        transform.Find("MoonStoneCounter/MoonStoneNumberText").GetComponent<TMP_Text>().text = MoonStoneNumber.ToString();
    }
    public void IncrementResources(int resourcesToAdd, string resourcesType)
    {
        switch (resourcesType)
        {
            case "coins":
                CoinsNumber = PlayerPrefs.GetInt("coins") + resourcesToAdd;
                PlayerPrefs.SetInt("coins", CoinsNumber);
                transform.Find("CoinCounter/CoinsNumberText").GetComponent<TMP_Text>().text = CoinsNumber.ToString();
                break;
            case "sapphire":
                SapphireNumber = PlayerPrefs.GetInt("sapphire") + resourcesToAdd;
                PlayerPrefs.SetInt("sapphire", SapphireNumber);
                transform.Find("SapphireCounter/SapphireNumberText").GetComponent<TMP_Text>().text = SapphireNumber.ToString();
                break;
            case "topaz":
                TopazNumber = PlayerPrefs.GetInt("topaz") + resourcesToAdd;
                PlayerPrefs.SetInt("topaz", TopazNumber);
                transform.Find("TopazCounter/TopazNumberText").GetComponent<TMP_Text>().text = TopazNumber.ToString();
                break;
            case "emerald":
                EmeraldNumber = PlayerPrefs.GetInt("emerald") + resourcesToAdd;
                PlayerPrefs.SetInt("emerald", EmeraldNumber);
                transform.Find("EmeraldCounter/EmeraldNumberText").GetComponent<TMP_Text>().text = EmeraldNumber.ToString();
                break;
            case "moonStone":
                MoonStoneNumber = PlayerPrefs.GetInt("moonStone") + resourcesToAdd;
                PlayerPrefs.SetInt("moonStone", MoonStoneNumber);
                transform.Find("MoonStoneCounter/MoonStoneNumberText").GetComponent<TMP_Text>().text = MoonStoneNumber.ToString();
                break;

            default:
                break;
        }
    }
    public void DecrementResources(int resourcesToRemove, string resourcesType)
    {
        CoinsNumber = PlayerPrefs.GetInt("coins") - resourcesToRemove;
        PlayerPrefs.SetInt("coins", CoinsNumber);
        GetComponent<TMP_Text>().text = CoinsNumber.ToString();
    }
}
