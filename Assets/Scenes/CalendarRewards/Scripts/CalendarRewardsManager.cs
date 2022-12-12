using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Common.Enums;
using Assets.Common.JsonModel;
using Assets.Common.Managers;
using Assets.Common.Models;
using UnityEngine;
using UnityEngine.UI;

public class CalendarRewardsManager : MonoBehaviour
{
    public GameObject RewardPrefab;
    public Transform RewardPrefabSpawnPoint;

    private List<GameObject> _calendarRewardList = new List<GameObject>();
    public RewardsIconsSO RewardsIconSO;

    #region Events Parameters
    private DateTime _startEventDate = new DateTime(2022, 12, 07);
    private List<GameObject> _eventRewardList = new List<GameObject>();
    #endregion
    void Start()
    {
        SpawnCalendarReward();
    }

    void Update()
    {
        WorkOnCalendarReward();
        WorkOnEventReward();
    }

    public void WorkOnCalendarReward()
    {
        var presentDay = (int)DateTime.Now.Day;

        if (_calendarRewardList.Count == 0)
            return;

        foreach (var calendarReward in _calendarRewardList)
        {
            var singleCalendarReward = calendarReward.GetComponent<CalendarRewardButton>();
            
            if ((presentDay == singleCalendarReward.Id) && (singleCalendarReward.RewardType.State != RewardState.Taken))
            {
                singleCalendarReward.RewardType.State = RewardState.Active;
                singleCalendarReward.AwardActivated();
            }
            else if ((presentDay != singleCalendarReward.Id) && (presentDay < singleCalendarReward.Id))
            {
                singleCalendarReward.RewardType.State = RewardState.Inactive;
                singleCalendarReward.AwardActivated();
            }
            else if((presentDay != singleCalendarReward.Id) && (presentDay > singleCalendarReward.Id) && 
                (singleCalendarReward.RewardType.State != RewardState.Taken))
            {
                singleCalendarReward.RewardType.State = RewardState.Loosed;
                singleCalendarReward.AwardLoosed();
            }
            else
            {
                singleCalendarReward.AwardTaked();
            }
        }
    }

    public void WorkOnEventReward()
    {
        if (_eventRewardList.Count == 0)
            return;

        var presentDay = (int)DateTime.Now.Day;
        var firstDayOfEvent = _startEventDate;
        
        for (int i = 0; i < _eventRewardList.Count; i++)
        {
            var singleEventReward = _eventRewardList[i].GetComponent<CalendarRewardButton>();
            
            if (i > 0 && _eventRewardList[i - 1].GetComponent<CalendarRewardButton>().RewardType.State == RewardState.Taken &&
                singleEventReward.RewardType.State == RewardState.Inactive)
            {
                singleEventReward.RewardType.State = RewardState.Active;
                singleEventReward.AwardActivated();
            }
            else if (singleEventReward.RewardType.State == RewardState.Inactive)
            {
                // singleEventReward.RewardType.State = RewardState.Inactive;
                singleEventReward.AwardActivated();
            }
            else if (singleEventReward.RewardType.State == RewardState.Taken)
            {
                singleEventReward.RewardType.State = RewardState.Taken;
                singleEventReward.AwardTaked();
            }
        }

        foreach(var eventReward in _eventRewardList.Skip(0)) { 
        }
    }
    public void SpawnCalendarReward()
    {
        var fileName = "Assets/Configuration/CallendarAwardPP.json";

        if (_calendarRewardList.Count != 0)
            return;

        foreach (var eventReward in _eventRewardList)
        {
            Destroy(eventReward);
        }
        _eventRewardList.Clear();

        var rewards = RewardEventManager.LoadCalendarRewards(fileName);

        foreach (var reward in rewards.OrderBy(reward => reward.Day))
        {
            var spawnCalendarReward = Instantiate(RewardPrefab, RewardPrefabSpawnPoint);
            _calendarRewardList.Add(spawnCalendarReward);
            
            var singleSpawnCalendarReward = spawnCalendarReward.GetComponent<CalendarRewardButton>();
            singleSpawnCalendarReward.Id = reward.Day;
            singleSpawnCalendarReward.dayNumberText.text = $"Day {reward.Day}";
            singleSpawnCalendarReward.awardAmountText.text = reward.Amount.ToString();
            singleSpawnCalendarReward.awardImage.sprite = RewardsIconSO.GetIcon(reward.Type);
            singleSpawnCalendarReward.RewardType.Type = reward.Type;
            singleSpawnCalendarReward.RewardType.Amount = reward.Amount;
            singleSpawnCalendarReward.RewardType.State = reward.State;
        }
    }

    public void SpawnEventReward()
    {
        if (_eventRewardList.Count != 0)
            return;

        foreach (var calendarReward in _calendarRewardList)
        {
           Destroy(calendarReward);
        }
        _calendarRewardList.Clear();
        
        var fileName = "Assets/Configuration/CalendarRewardsEvents/ArchontEventAwards.json";
        var rewards = RewardEventManager.LoadCalendarRewards(fileName);

        foreach (var reward in rewards)
        {
            var spawnEventReward = Instantiate(RewardPrefab, RewardPrefabSpawnPoint);
            _eventRewardList.Add(spawnEventReward);

            var singleSpawnEventReward = spawnEventReward.GetComponent<CalendarRewardButton>();
            singleSpawnEventReward.Id = reward.Day;
            singleSpawnEventReward.dayNumberText.text = $"Day {reward.Day}";
            singleSpawnEventReward.awardAmountText.text = reward.Amount.ToString();
            singleSpawnEventReward.awardImage.sprite = RewardsIconSO.GetIcon(reward.Type);
            singleSpawnEventReward.RewardType.Type = reward.Type;
            singleSpawnEventReward.RewardType.Amount = reward.Amount;
            singleSpawnEventReward.RewardType.State = reward.State;

        }
    }

    public void TakeAward()
    {
        if (_calendarRewardList.Count > 0)
        {
            foreach (var eventReward in _calendarRewardList)
            {
                var singleCalendarReward = eventReward.GetComponent<CalendarRewardButton>();

                if (singleCalendarReward.RewardType.State == RewardState.Active)
                {
                    PlayerPreferences.SaveResourceByType(singleCalendarReward.RewardType.Type.ToString(),
                        singleCalendarReward.RewardType.Amount);

                    singleCalendarReward.RewardType.State = RewardState.Taken;
                }
            }
        }
        else if(_eventRewardList.Count > 0)
        {
            foreach (var eventReward in _eventRewardList)
            {
                var singleEventReward = eventReward.GetComponent<CalendarRewardButton>();

                if (singleEventReward.RewardType.State == RewardState.Active)
                {
                    PlayerPreferences.SaveResourceByType(singleEventReward.RewardType.Type.ToString(),
                        singleEventReward.RewardType.Amount);

                    singleEventReward.RewardType.State = RewardState.Taken;
                }
            }
        }
        
    }
}
