using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Award : MonoBehaviour
{
    protected float Animation;
    //public Transform AwarsSlotTransform;
    public float Speed;
    void Start()
    {
        
    }

    void Update()
    {
        ShowAwards();

    }
    void ShowAwards()
    {      
        
        var awarsSlot = GameObject.Find("AwarsSlot");

        var destination = awarsSlot.transform.position - this.transform.position;
        GetComponent<Rigidbody2D>().velocity = destination.normalized * Speed;
            
               

        this.transform.SetParent(awarsSlot.transform);
        if (this.transform.position == awarsSlot.transform.position)
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
