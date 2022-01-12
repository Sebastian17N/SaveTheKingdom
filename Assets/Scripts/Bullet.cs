using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletType
{
    public Sprite Sprite;
    public float ShootingDuration = 0.5f;
    public float Speed = 6f;
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{
    public void Configure(BulletType bulletType)
    {
        GetComponent<SpriteRenderer>().sprite = bulletType.Sprite;
        GetComponent<Rigidbody2D>().velocity = Vector3.right * bulletType.Speed;
    }
}
