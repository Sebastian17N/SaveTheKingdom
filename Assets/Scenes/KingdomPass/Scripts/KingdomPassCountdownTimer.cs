using System.Collections;
using UnityEngine;
using TMPro;
using System;
using static UnityEditor.ShaderData;
using System.IO;
using Assets.Common.Managers;
using Assets.Common.Models;
using Assets.Common.JsonModel;

public class KingdomPassCountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI CountdownTimerText;
    public KingdomPassJsonModel KingdomPassJsonModel;
    private int days = 10;
    private int hours = 10;
    private void Awake()
    {
        TakeCurrentFile();
        TimeLeftCounter();
        CountdownTimerText.GetComponent<TextMeshProUGUI>().text = $"{days}d {hours}h";
    }
    void Update()
    {
       StartCoroutine(TimeCounter());
    }

    IEnumerator TimeCounter()
    {
        yield return new WaitForSeconds(1);
        TimeLeftCounter();
        CountdownTimerText.GetComponent<TextMeshProUGUI>().text = $"{days}d {hours}h";
    }

    public void TimeLeftCounter()
    {
        var totalHours = (DateTime.Parse(KingdomPassJsonModel.EventEndDateTime) - DateTime.Now).TotalHours;
        days = (int)totalHours / 24;
        hours = (int)totalHours % 24;
    }
    private void TakeCurrentFile()
    {
        var directoryInfo = new DirectoryInfo("Assets/Configuration/KingdomPass");
        var files = directoryInfo.GetFiles("*.json");

        foreach (var file in files)
        {
            var rewardsFile = RewardEventManager.LoadKingdomPassRewards(file.FullName);

            if (DateTime.Today >= DateTime.Parse(rewardsFile.EventStartDateTime) && DateTime.Today <= DateTime.Parse(rewardsFile.EventEndDateTime))
            {
                KingdomPassJsonModel = rewardsFile;
                break;
            }
        }
    }
}
