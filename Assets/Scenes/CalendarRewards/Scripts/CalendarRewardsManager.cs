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
    public GameObject CalendarRewardPrefab;
    public Transform CalendarRewardPrefabSpawnPoint;
    private List<GameObject> _calendarRewardList = new List<GameObject>();
    public RewardsIconsSO RewardsIconSO;

    #region Events Parameters
    private DateTime _startEventDate = new DateTime(2022, 11, 26);
    private int _eventDaysNumber = 10;
    private Transform _eventRewardPrefabSpawnPoint;
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
            
            if ((presentDay == singleCalendarReward.Id) && (singleCalendarReward.RewardState != RewardState.Taken))
            {
                singleCalendarReward.RewardState = RewardState.Active;
                singleCalendarReward.AwardActivated();
            }
            else if ((presentDay != singleCalendarReward.Id) && (presentDay < singleCalendarReward.Id) && 
                (singleCalendarReward.RewardState != RewardState.Taken))
            {
                singleCalendarReward.RewardState = RewardState.Inactive;
                singleCalendarReward.AwardActivated();
            }
            else if((presentDay != singleCalendarReward.Id) && (presentDay > singleCalendarReward.Id) && 
                (singleCalendarReward.RewardState != RewardState.Taken))
            {
                singleCalendarReward.RewardState = RewardState.Inactive;
                singleCalendarReward.RewardState = RewardState.Loosed;
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
        //var monthDaysTotalNumber = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

        foreach (var reward in rewards.OrderBy(reward => reward.Day))
        {
            var spawnCalendarReward = Instantiate(CalendarRewardPrefab, CalendarRewardPrefabSpawnPoint);
            _calendarRewardList.Add(spawnCalendarReward);
            
            var singleSpawnCalendarReward = spawnCalendarReward.GetComponent<CalendarRewardButton>();
            singleSpawnCalendarReward.Id = reward.Day;
            singleSpawnCalendarReward.dayNumberText.text = $"Day {reward.Day}";
            singleSpawnCalendarReward.awardAmountText.text = reward.Amount.ToString();
            singleSpawnCalendarReward.awardImage.sprite = RewardsIconSO.GetIcon(reward.Type);
            singleSpawnCalendarReward.RewardType.Type = reward.Type;
            singleSpawnCalendarReward.RewardType.Amount = reward.Amount;
            singleSpawnCalendarReward.RewardState = reward.State;
        }
    }
    
    public void TakeAward()
    {

        foreach (var calendarReward in _calendarRewardList)
        {
            var singleCalendarReward = calendarReward.GetComponent<CalendarRewardButton>();

            if (singleCalendarReward.RewardState == RewardState.Active)
            {
                PlayerPreferences.SaveResourceByType(singleCalendarReward.RewardType.Type.ToString(), 
                    singleCalendarReward.RewardType.Amount);
                
                singleCalendarReward.RewardState = RewardState.Taken;
            }
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

        for (int i = 1; i < _eventDaysNumber; i++)
        {
            var spawnEventReward = Instantiate(CalendarRewardPrefab, CalendarRewardPrefabSpawnPoint);
            _eventRewardList.Add(spawnEventReward);
            var singleSpawnEventReward = spawnEventReward.GetComponent<CalendarRewardButton>();
            singleSpawnEventReward.Id = i;
            singleSpawnEventReward.dayNumberText.text = $"Day {i}";
        }
    }
}
