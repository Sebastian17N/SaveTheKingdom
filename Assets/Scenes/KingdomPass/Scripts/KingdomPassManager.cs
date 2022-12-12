using Assets.Common.Enums;
using Assets.Common.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingdomPassManager : MonoBehaviour
{
    public GameObject KingdomPassAwardsButtonsPrefab;
    public List<GameObject> KingdomPassAwardsButtonsList = new List<GameObject>();
    public Transform KingdomPassAwardsButtonsPrefabSpawnPoint;
    public RewardsIconsSO RewardsIconSO;
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

            if (singlePassObject.RewardType.State == RewardState.Taken)
            {
                singlePassObject.FreeAwardTaked();
                singlePassObject.PremiumAwardTaked();
            }
            else if (passPoints >= singlePassObject.passPointsRequiredToActivateAward)
            {
                singlePassObject.ActivateAward();
            }
        }
    }
   
    public void SpawnKingdomPassAwardsButtons()
    {
        var fileName = "Assets/Configuration/KingdomPass/KingdomPassReward.json";
        var rewards = RewardEventManager.LoadCalendarRewards(fileName);

        foreach (var reward in rewards)
        {
            var kingdomPassAwardsButton = Instantiate(KingdomPassAwardsButtonsPrefab, KingdomPassAwardsButtonsPrefabSpawnPoint);
            KingdomPassAwardsButtonsList.Add(kingdomPassAwardsButton);
            var singleKingdomPassAwardsButton = kingdomPassAwardsButton.GetComponent<KingdomPassAwardsButtons>();
            singleKingdomPassAwardsButton.ordinalNumberText.text = reward.Day.ToString();
            singleKingdomPassAwardsButton.passPointsRequiredToActivateAward = (reward.Day * 100);
            singleKingdomPassAwardsButton.freeAwardAmountText.text = reward.Amount.ToString();
            singleKingdomPassAwardsButton.freeAwardImage.sprite = RewardsIconSO.GetIcon(reward.Type);
            singleKingdomPassAwardsButton.RewardType.Type = reward.Type;
            singleKingdomPassAwardsButton.RewardType.Amount = reward.Amount;
            singleKingdomPassAwardsButton.RewardType.State = reward.State;

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
