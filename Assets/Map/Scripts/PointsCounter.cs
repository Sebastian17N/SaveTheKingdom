using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsCounter : MonoBehaviour
{
    int Points = 0;
    void Start()
    {
        SavePoints();
        RefreshText();        
    }

    public void IncrementPoints()
    {
        Points++;
        SavePoints();
        RefreshText();
    }
    void SavePoints()
    {
        PlayerPrefs.SetInt("current_points", Points);
    }
    void RefreshText()
    {
        GetComponent<TMP_Text>().text = Points.ToString() + " points";
    }
}
