using Assets.Common.JsonModel;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ShopCountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI CountdownTimerText;
    private DateTime tommorow => DateTime.Today.AddDays(1);
    private int hours;
    private int minutes;

    void Start()
    {
        TimeLeftCounter();
        ShowTimeLeft();
    }

    void Update()
    {
        StartCoroutine(TimeCounter());
    }
    IEnumerator TimeCounter()
    {
        yield return new WaitForSeconds(1);
        TimeLeftCounter();
        ShowTimeLeft();
    }
    private void ShowTimeLeft()
    {
        CountdownTimerText.GetComponent<TextMeshProUGUI>().text = $"TIME TO REFRESH {hours:D2}H : {minutes:D2}M";
    }
    private void TimeLeftCounter()
    {
        var totalTime = tommorow - DateTime.Now;
        hours = totalTime.Hours;
        minutes = totalTime.Minutes + 1;
    }
}
