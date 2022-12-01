using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarRewardsManager : MonoBehaviour
{
    public GameObject CalendarRewardPrefab;
    public Transform CalendarRewardPrefabSpawnPoint;
    private List<GameObject> _calendarRewardList = new List<GameObject>();
    
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
            var singleCalendarReward = calendarReward.GetComponent<CalendarReward>();
            
            if ((presentDay == singleCalendarReward.Id) && (!singleCalendarReward.isAwardTaked))
            {
                singleCalendarReward.isAwardActivated = true;
                singleCalendarReward.AwardActivated();
            }
            else if ((presentDay != singleCalendarReward.Id) && (presentDay < singleCalendarReward.Id) && (!singleCalendarReward.isAwardTaked))
            {
                singleCalendarReward.isAwardActivated = false;
                singleCalendarReward.AwardActivated();
            }
            else if((presentDay != singleCalendarReward.Id) && (presentDay > singleCalendarReward.Id) && (!singleCalendarReward.isAwardTaked))
            {
                singleCalendarReward.isAwardActivated = false;
                singleCalendarReward.isAwardLoosed = true;
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
        if (_calendarRewardList.Count != 0)
            return;

        foreach (var eventReward in _eventRewardList)
        {
            Destroy(eventReward);
        }
        _eventRewardList.Clear();

        var monthDaysTotalNumber = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        
        for (int i = 1; i < monthDaysTotalNumber +1; i++)
        {
            var spawnCalendarReward = Instantiate(CalendarRewardPrefab, CalendarRewardPrefabSpawnPoint);
            _calendarRewardList.Add(spawnCalendarReward);
            var singleSpawnCalendarReward = spawnCalendarReward.GetComponent<CalendarReward>();
            singleSpawnCalendarReward.Id = i;
            singleSpawnCalendarReward.dayNumberText.text = $"Day {i.ToString()}";
        }
    }
    public void TakeAward()
    {
        foreach (var calendarReward in _calendarRewardList)
        {
            var singleCalendarReward = calendarReward.GetComponent<CalendarReward>();

            if (singleCalendarReward.isAwardActivated)
            {
                singleCalendarReward.isAwardTaked = true;
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
            var singleSpawnEventReward = spawnEventReward.GetComponent<CalendarReward>();
            singleSpawnEventReward.Id = i;
            singleSpawnEventReward.dayNumberText.text = $"Day {i.ToString()}";
        }
    }

    //public void ActivateAward(int dayNumber, bool isAwardActivated)
    //{

    //    if ((int)DateTime.Now.Day == dayNumber)
    //    {
    //        isAwardActivated = true;
    //    }
    //    else
    //    {
    //        isAwardActivated = false;
    //    }
    //}
}
