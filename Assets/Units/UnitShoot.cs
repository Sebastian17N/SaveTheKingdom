using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitShoot : MonoBehaviour
{
    [SerializeField]
    public GameObject BulletPrefab;

    [SerializeField]
    public BulletType BulletType;

    [SerializeField]
    public float AtackSpeed;

    private float _lastShootTime = 0f;
    
    public void Update()
    {
        ShootBullet();
    }

    private void ShootBullet()
    {
        if (Time.timeSinceLevelLoad - _lastShootTime < AtackSpeed)
            return;

        var bullet = Instantiate(BulletPrefab);
        bullet.transform.position = transform.position + Vector3.right * 1.5f;

        bullet.GetComponent<Bullet>().Configure(BulletType);

        _lastShootTime = Time.timeSinceLevelLoad;
    }
}
