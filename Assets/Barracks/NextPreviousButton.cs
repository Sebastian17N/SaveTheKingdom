using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPreviousButton : MonoBehaviour
{
    public void NextUnit()
    {
        GameObject.FindObjectOfType<BarracksGameManager>().NextUnit();
    }
    public void PreviousUnit()
    {
        GameObject.FindObjectOfType<BarracksGameManager>().PreviousUnit();
    }
}
