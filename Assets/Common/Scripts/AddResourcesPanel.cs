using Assets.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddResourcesPanel : MonoBehaviour
{
    public GameObject BuyResuorcesButton;
    public List<Sprite> SapphireIncrementList;
    public List<Sprite> TopazIncrementList;
    public List<Sprite> EmeraldIncrementList;
    public RewardType Type;
    
    private void Start()
    {
        CreateBuyResuorcesButtonByType();
    }
    private void CreateBuyResuorcesButtonByType()
    {
        switch (Type)
        {
            case RewardType.Sapphires:
                CreateBuyResuorcesButton(SapphireIncrementList);
                break;
            case RewardType.Topazes:
                CreateBuyResuorcesButton(TopazIncrementList);
                break;
            case RewardType.Emeralds:
                CreateBuyResuorcesButton(EmeraldIncrementList);
                break;
            default:
                break;
        }
    }
    private void CreateBuyResuorcesButton(List<Sprite> incrementList)
    {
        for (int i = 0; i < 3; i++)
        {
            var buyResuorcesButton = Instantiate(BuyResuorcesButton, transform.GetChild(0).transform);
            buyResuorcesButton.GetComponent<Image>().sprite = incrementList[i];
            buyResuorcesButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ((i+1) * 100).ToString();
            buyResuorcesButton.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ((i+1) * 10).ToString();
            buyResuorcesButton.GetComponent<BuyResuorcesButton>().Type = Type;
        }
    }
}
