using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public ScriptableEnemy ScriptableEnemies;
    public float Speed = 2f;    
    public float Durability = 10;
    public int spawnTime;
    bool isWalking = true;
    bool isAttacking;
    bool isTarget;
    
    void Start()
    {
        
    }
        
    void Update()
    {
        Walking();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.gameObject;
        var bullet = enemy.GetComponent<Bullet>();

        var target = enemy.GetComponent<UnitShoot>();

        if (bullet != null)
        {
            DecreaseDurability(bullet.Damage);
            Destroy(enemy);
        }

        if (target != null)
        {
            isWalking = false;
            Atack();
            
        }

    }
    private void Walking()
    {
        if (isWalking)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.left * Speed;
        }
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
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
}

