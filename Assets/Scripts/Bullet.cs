using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{    

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.right * 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
