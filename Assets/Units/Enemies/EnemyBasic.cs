using System;
using UnityEngine;

[System.Serializable]   
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBasic : MonoBehaviour
{
    public ScriptableEnemy ScriptableEnemies;
    public float Speed = 2f;    
    public float Durability = 10;
    public int SpawnTime;

    private bool _isWalking = true;
    private bool _isAttacking;
    private bool _isTarget;
        
    void Update()
    {
        Walking();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.gameObject;
        var bullet = enemy.GetComponent<Bullet>();
        var target = enemy.GetComponent<UnitBasic>();

        if (bullet != null)
        {
            DecreaseDurability(bullet.Damage);
            Destroy(enemy);
        }

        if (target != null)
        {
            _isWalking = false;
            Attack();            
        }

    }
    private void Walking()
    {
        if (_isWalking)
            GetComponent<Rigidbody2D>().velocity = Vector2.left * Speed;
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
    }
    private void Attack()
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

