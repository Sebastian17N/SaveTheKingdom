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
    public void ActivateAward(int dayNumber, bool isAwardActivated)
    {

        if ((int)DateTime.Now.Day == dayNumber)
        {
            isAwardActivated = true;
        }
        else
        {
            isAwardActivated = false;
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
}
