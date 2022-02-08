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
    public GameObject MoonStonePrefab;
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
       
    }
    void Update()
    {
        
        Walking();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collider = collision.gameObject;
        var bullet = collider.GetComponent<Bullet>();
        var unit = collider.GetComponent<UnitBasic>();

        if (bullet != null)
        {
            DecreaseDurability(bullet.Damage);
            Destroy(collider);

            return;
        }

        if (unit != null && !unit.IsDragged)
        {
            _isWalking = false;
            Target = collision.gameObject;
            StartCoroutine(Attack());

            return;
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
            var unitLives = Target.GetComponent<UnitBasic>().DecreaseHealth(AttackDamage);

            if (!unitLives)
			{
                _isWalking = true;
                yield return new WaitForSeconds(0.5f);
			}
        }
        
        yield return new WaitForSeconds(3f);
        StartCoroutine(Attack());
    }

    private void DecreaseDurability(float amount)
    {
        Durability -= amount;

        if (Durability <= 0)
        {
            FindObjectOfType<GameManager>().NumberOfEnemiesLeft--;
            Destroy(gameObject);
            var point = Instantiate(MoonStonePrefab);
            point.transform.position = transform.position;
        }
    }    
}

