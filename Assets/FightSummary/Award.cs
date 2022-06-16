using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Award : MonoBehaviour
{
    protected float Animation;
    public Transform AwarsSlotTransform;
    public float Speed;
    void Start()
    {
        ShowAwards();
    }

    void Update()
    {


        //Animation += Time.deltaTime;
        //Animation = Animation % 5f;
        //transform.position = MathParabola.Parabola
        //    (Vector2.zero, Vector2.left * 5f, 1f, Animation / 3f);
    }
    void ShowAwards()
    {
        //var animObject = Instantiate(gameObject, CanvasTransform);
        //animObject.name = name;
        //animObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
        //animObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
        //animObject.transform.position = transform.position;
        //animObject.GetComponent<UnitIcon>().Slot = foundSlot;

        ////var destination = foundSlot.transform.position - animObject.transform.position;
        //animObject.GetComponent<Rigidbody2D>().velocity = destination.normalized * Speed; 

        var awarsSlot = GameObject.Find("AwarsSlot");

        var destination = GetComponent<Rigidbody2D>().velocity = 
            awarsSlot.transform.position - this.transform.position ;
        
    }
}
