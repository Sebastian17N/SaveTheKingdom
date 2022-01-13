using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
