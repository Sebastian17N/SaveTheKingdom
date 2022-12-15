using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class KingdomPassCountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI CountdownTimerText;
    public bool isEventEnd = false;
    public DateTime endEventDate = new DateTime(2022, 12, 24, 17, 00, 00);
    public int days = 10;
    public int hours = 10;

    void Update()
    {
        if (days != 0 && hours != 0)
        {
            StartCoroutine(TimeCounter());
        }
        else
        {
            isEventEnd = true;
        }
    }

    IEnumerator TimeCounter()
    {
        yield return new WaitForSeconds(1);
        TimeLeftCounter();
        CountdownTimerText.GetComponent<TextMeshProUGUI>().text = $"{days}d {hours}h";
    }

    public void TimeLeftCounter()
    {
        var totalHours = (endEventDate - DateTime.Now).TotalHours;
        days = (int)totalHours / 24;
        hours = (int)totalHours % 24;
    }
}
