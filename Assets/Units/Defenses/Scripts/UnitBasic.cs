using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBasic : MonoBehaviour
{
    [SerializeField]
    public GameObject BulletPrefab;

    [SerializeField]
    public BulletType BulletType;

    /// <summary>
    /// Should be taken from UnitScriptableObject.
    /// </summary>
    public float AttackSpeed;
    public float Health;
    public bool IsDragged = true;
    private float _lastShootTime = 0f;
    public GameObject Unit;
    public void Update()
    {
        ShootBullet();
    }

    private void ShootBullet()
    {
        if (IsDragged)
            return;

        if (Time.timeSinceLevelLoad - _lastShootTime < AttackSpeed)
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
