using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingdomPassManager : MonoBehaviour
{
    public GameObject KingdomPassAwardsButtonsPrefab;
    public List<GameObject> KingdomPassAwardsButtonsList = new List<GameObject>();
    public Transform KingdomPassAwardsButtonsPrefabSpawnPoint;
    public bool isKingdomPassActivated = false;
    public int passPoints = 0;
    private void Awake()
    {
        SpawnKingdomPassAwardsButtons();
    }
    void Update()
    {
        WorkOnKingdomPassAwardsButtons();

    }
    public void WorkOnKingdomPassAwardsButtons()
    {
        foreach (var kingdomPassAwards in KingdomPassAwardsButtonsList)
        {
            var singlePassObject = kingdomPassAwards.GetComponent<KingdomPassAwardsButtons>();
            singlePassObject.AwardActivated();
            singlePassObject.KingdomPassActivated();
            singlePassObject.FreeAwardTaked();
            singlePassObject.PremiumAwardTaked();

            if (passPoints >= singlePassObject.passPointsRequiredToActivateAward)
            {
                singlePassObject.ActivateAward();
            }
        }
    }
   
    public void SpawnKingdomPassAwardsButtons()
    {
        for (int i = 1; i < 10; i++)
        {
            var kingdomPassAwardsButton = Instantiate(KingdomPassAwardsButtonsPrefab, KingdomPassAwardsButtonsPrefabSpawnPoint);
            KingdomPassAwardsButtonsList.Add(kingdomPassAwardsButton);
            var singleKingdomPassAwardsButton = kingdomPassAwardsButton.GetComponent<KingdomPassAwardsButtons>();
            singleKingdomPassAwardsButton.ordinalNumberText.text = $"{i}";
            singleKingdomPassAwardsButton.passPointsRequiredToActivateAward = (i * 100);
        }
    }
    public void ActivateKingdomPass()
    {
        isKingdomPassActivated = true;
    }
    public void IncrementPassPoints()
    {

    }
}
