using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
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
        var enemy = collision.gameObject;
        var bullet = enemy.GetComponent<Bullet>();

        if(bullet != null)
        {
            Durability--;
            Destroy(enemy);

            if(Durability <= 0)
                Destroy(gameObject);
        }
    }
}
