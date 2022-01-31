using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{
    public float Damage = 1f;
    public void Configure(BulletType bulletType)
    {
        Damage = bulletType.Damage;

        GetComponent<SpriteRenderer>().sprite = bulletType.Sprite;
        GetComponent<Rigidbody2D>().velocity = Vector3.right * bulletType.Speed;
    }
}
