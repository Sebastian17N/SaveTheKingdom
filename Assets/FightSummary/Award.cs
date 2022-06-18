using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Award : MonoBehaviour
{
    protected float Animation;
    public float Speed;
    public int Coins = 0;

    void Start()
    {
        
    }

    void Update()    
    {
        ShowAwards();
        if (transform.parent == GameObject.Find("AwardsSlot").transform)
        {
            ShowCoinsAmount();
        }

    }
    void ShowAwards()
    {    
        var awarsSlot = GameObject.Find("AwardsSlot");

        if (transform.parent == awarsSlot.transform)
            return;

        if (Vector2.Distance(awarsSlot.transform.position, transform.position) < 10f)
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.transform.SetParent(awarsSlot.transform);

            return;
        }

        var destination = awarsSlot.transform.position - this.transform.position;
        GetComponent<Rigidbody2D>().velocity = destination.normalized * Speed;
              
    }
    public void ShowCoinsAmount()
    {
        GetComponentInChildren<TMP_Text>().text = Coins.ToString();
    }
}
