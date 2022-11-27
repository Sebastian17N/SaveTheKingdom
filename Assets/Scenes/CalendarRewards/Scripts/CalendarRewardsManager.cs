using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarRewardsManager : MonoBehaviour
{
    public GameObject CalendarRewardPrefab;
    public Transform CalendarRewardPrefabSpawnPoint;
    public List<GameObject> CalendarRewardList = new List<GameObject>();
    
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
    }

    public void WorkOnCalendarReward()
    {
        var day = (int)DateTime.Now.Day;

        if (CalendarRewardList == null)
            return;

        foreach (var calendarReward in CalendarRewardList)
        {
            var singleCalendarReward = calendarReward.GetComponent<CalendarReward>();
            
            if ((day == singleCalendarReward.Id) && (!singleCalendarReward.isAwardTaked))
            {
                singleCalendarReward.isAwardActivated = true;
                singleCalendarReward.AwardActivated();
            }
            else if ((day != singleCalendarReward.Id) && (day < singleCalendarReward.Id) && (!singleCalendarReward.isAwardTaked))
            {
                singleCalendarReward.isAwardActivated = false;
                singleCalendarReward.AwardActivated();
            }
            else if((day != singleCalendarReward.Id) && (day > singleCalendarReward.Id) && (!singleCalendarReward.isAwardTaked))
            {
                singleCalendarReward.isAwardActivated = false;
                singleCalendarReward.AwardLoosed();
            }
            else
            {
                singleCalendarReward.AwardTaked();
            }
        }
    }

    public void SpawnCalendarReward()
    {
        var monthDaysTotalNumber = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        
        for (int i = 1; i < monthDaysTotalNumber +1; i++)
        {
            var spawnCalendarReward = Instantiate(CalendarRewardPrefab, CalendarRewardPrefabSpawnPoint);
            CalendarRewardList.Add(spawnCalendarReward);
            var singleSpawnCalendarReward = spawnCalendarReward.GetComponent<CalendarReward>();
            singleSpawnCalendarReward.Id = i;
            singleSpawnCalendarReward.dayNumberText.text = $"Day {i.ToString()}";
        }
    }
    public void TakeAward()
    {
        foreach (var calendarReward in CalendarRewardList)
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
        foreach (var calendarReward in CalendarRewardList)
        {
           Destroy(calendarReward);
        }
        //CalendarRewardList.Clear();

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
