using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitShoot : MonoBehaviour
{
    [SerializeField]
    GameObject BulletPrefab;

    [SerializeField]
    BulletType BulletType;

    [SerializeField]
    float AtackSpeed;

    void Start()
    {
        
    }

    float lastShootTime = 0f;
    
    void Update()
    {
        ShootBullet();
    }

    private void ShootBullet()
    {
        if (Time.timeSinceLevelLoad - lastShootTime < AtackSpeed)
            return;

        var bullet = Instantiate(BulletPrefab);
        bullet.transform.position = transform.position + Vector3.right * 1.5f;

        bullet.GetComponent<Bullet>().Configure(BulletType);

        lastShootTime = Time.timeSinceLevelLoad;
    }
}
