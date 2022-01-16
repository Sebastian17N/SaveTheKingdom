using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemi : MonoBehaviour
{
    [SerializeField]
    float Speed = 2f;
    [SerializeField]
    float Durability = 10;
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.left * Speed;
    }
        
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemie = collision.gameObject;
        var bullet = enemie.GetComponent<Bullet>();

        if(bullet != null)
        {
            Durability--;
            Destroy(enemie);

            if(Durability <= 0)
                Destroy(gameObject);
        }
    }
}
