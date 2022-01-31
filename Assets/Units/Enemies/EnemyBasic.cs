using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBasic : MonoBehaviour
{
    public ScriptableEnemy ScriptableEnemies;
    public GameObject Target;
    public float Speed = 2f;    
    public float Durability = 10;
    public int SpawnTime;
    public float AttackDamage;
    public float attackInterval;    
    private bool _isWalking = true;
    private bool _isAttacking;
    private bool _isTarget;

    private void Start()
    {
        attackInterval = ScriptableEnemies.AttackInterval;
    }
    void Update()
    {
        
        Walking();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.gameObject;
        var bullet = enemy.GetComponent<Bullet>();
        var unit = enemy.GetComponent<UnitBasic>();

        if (bullet != null)
        {
            DecreaseDurability(bullet.Damage);
            Destroy(enemy);
        }

        if (unit != null)
        {
            _isWalking = false;
            Target = collision.gameObject;
            StartCoroutine(Attack());
        }

    }
    private void Walking()
    {
        if (_isWalking)
            GetComponent<Rigidbody2D>().velocity = Vector2.left * Speed;
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
    }
    public IEnumerator Attack()
    {
       
        if (Target != null)
        {
            Target.GetComponent<UnitBasic>().DecreaseHealth(AttackDamage);
        }
        
        yield return new WaitForSeconds(attackInterval);
        StartCoroutine(Attack());
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

