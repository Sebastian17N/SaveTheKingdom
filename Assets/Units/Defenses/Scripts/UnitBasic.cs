using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBasic : MonoBehaviour
{
    
    public GameObject BulletPrefab;    
    public BulletType BulletType;    
    public float AtackSpeed;
    public float Health;

    private float _lastShootTime = 0f;
    public GameObject Unit;
    public void Update()
    {
        ShootBullet();
    }

    private void ShootBullet()
    {
        if (Time.timeSinceLevelLoad - _lastShootTime < AtackSpeed)
            return;

        var bullet = Instantiate(BulletPrefab);
        bullet.transform.position = transform.position + Vector3.right * 1.5f + Vector3.down * 0.5f;

        bullet.GetComponent<Bullet>().Configure(BulletType);

        _lastShootTime = Time.timeSinceLevelLoad;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var unit = collision.gameObject;

        var enemy = unit.GetComponent<EnemyBasic>();
       
        if (enemy != null)
        {
            //DecreaseDurability(enemy.AtackDamage);
        }
    }
    private void DecreaseDurability(float amount)
    {
        Health -= amount;

        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
