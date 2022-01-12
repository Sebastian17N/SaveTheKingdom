using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitShot : MonoBehaviour
{
    [SerializeField]
    GameObject BulletPrefab;

    [SerializeField]
    float ShootPeriod = 0.5f;

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
        if (Time.timeSinceLevelLoad - lastShootTime < ShootPeriod)
            return;

        var bullet = Instantiate(BulletPrefab);
        bullet.transform.position = transform.position + Vector3.right * 1.5f;

        lastShootTime = Time.timeSinceLevelLoad;
    }
}
