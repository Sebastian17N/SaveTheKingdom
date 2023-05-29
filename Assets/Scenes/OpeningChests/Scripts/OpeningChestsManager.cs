using Assets.Scenes.FightSummary.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningChestsManager : MonoBehaviour
{
    public GameObject[] Chests;
    public GameObject RewardsSlot;
    public GameObject Reward;
    public List<GameObject> ChestRewardsList;
    public GameObject FlipAllRewardButton;
    public ChestOfferJsonModel ChestOfferJsonModel;
    void Start()
    {
        ChestOfferJsonModel = ShopJsonLoader.LoadChestOfferJsonModel();
        ActivateChest();
        FlipAllRewardButton.SetActive(false);
    }

    private void ActivateChest()
    {
        for (int i = 0; i < Chests.Length; i++)
        {
            if (Chests[i].GetComponent<ChestManager>().ChestClassification == ChestOfferJsonModel.ChestClassification)
            {
                Chests[i].SetActive(true);
            }
        }
    }
    public void ActivateReward()
    {
        RewardsSlot.SetActive(true);

        for (int i = 0; i < 5; i++)
        {
            var award = Instantiate(Reward, RewardsSlot.transform);
            //TO DO dodaj skrypt / algorytm przyznaj¹cy ró¿ne nagrody za ró¿ne skrzynie, w zale¿noœci od postêpów gracza
            //award = GetComponent<ChestReward>().GameLogo;
            
            ChestRewardsList.Add(award);
        }

        StartCoroutine(InstantiateRewards());
        FlipAllRewardButton.SetActive(true);

    }
    private IEnumerator InstantiateRewards()
    {
        foreach (var chestReward in ChestRewardsList)
        {
            yield return new WaitForSeconds(0.3f);
            chestReward.GetComponent<Animator>().enabled = true;
        }
    }
    public void FlipAllReward()
    {
        StartCoroutine(FlipSingleReward());
    }
    private IEnumerator FlipSingleReward()
    {
        foreach (var chestReward in ChestRewardsList)
        {
            if (chestReward.GetComponent<ChestReward>().isFlipped == true)
                continue;

            yield return new WaitForSeconds(0.2f);
            chestReward.GetComponent<ChestReward>().isFlipped = true;
        }
    }

}
