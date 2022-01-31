using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBasic : MonoBehaviour
{
    public ScriptableEnemy ScriptableEnemies;
    UnitBasic UnitBasic;
    GameObject Target;
    public float Speed = 2f;    
    public float EnemyHealth = 10;
    public float AtackDamage;
    public float AtackInterval;
    public int spawnTime;
    bool isWalking = true;
    bool isAttacking = true;
    bool isTarget;
            
    void Update()
    {
        Walking();

    }
    private void OnTriggerStay2D(Collider2D collision)
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
            StartCoroutine(Atack());
            //target.GetComponent<UnitBasic>().Health -= AtackDamage * Time.deltaTime;
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
    public IEnumerator Atack()
    {
        isWalking = false;
        Target.GetComponent<UnitBasic>().Health -= AtackDamage;
        //if (Target != null)
        //{
            
        //}
        //GetComponent<UnitBasic>().Health -= AtackDamage;
        
        yield return new WaitForSeconds(AtackInterval);
        StartCoroutine(Atack());
    }

    private void DecreaseDurability(float amount)
    {
        EnemyHealth -= amount;

        if (EnemyHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }    
}

