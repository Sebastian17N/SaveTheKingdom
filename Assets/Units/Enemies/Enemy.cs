using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public ScriptableEnemies ScriptableEnemies;
    public float Speed = 2f;    
    public float Durability = 10;
    public int spawnTime;
    public EnamiesType enemiesType;
    bool isWalking;
    bool isAttacking;
    bool isTarget;
    
    void Start()
    {
        
    }
        
    void Update()
    {
        if (isTarget == false)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.left * Speed;
        }   

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.gameObject;
        var bullet = enemy.GetComponent<Bullet>();
        var target = enemy.GetComponent<FieldManager>();

        if (bullet != null)
        {
            DecreaseDurability(bullet.Damage);
            Destroy(enemy);
        }

        if (target != null)
        {
            Atack();
            isTarget = true;    
            isWalking = false;
        }

    }

    private void Atack()
    {
        throw new NotImplementedException();
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

