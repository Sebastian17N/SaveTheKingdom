using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{    
    public float Speed = 2f;    
    public float Durability = 10;
    public int spawnTime;
    public EnamiesType enemiesType;

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

        if (bullet != null)
        {
            DecreaseDurability(bullet.Damage);
            Destroy(enemy);
        }
    }

    private void DecreaseDurability(float amount)
    {
        Durability -= amount;

        if (Durability <= 0)
        {
            Destroy(gameObject);
        }
    }
    public enum EnamiesType
    {
        Enemy_Basic,
        Enemy_Basic1,
        Enemy_Basic2
    }
}

