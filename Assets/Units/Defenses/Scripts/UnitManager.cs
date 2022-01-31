using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField]
    public GameObject BulletPrefab;

    [SerializeField]
    public BulletType BulletType;

    /// <summary>
    /// Should be taken from UnitScriptableObject.
    /// </summary>
    public float AttackSpeed;
    public bool IsDragged = true;

    private float _lastShootTime = 0f;
    
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
}
