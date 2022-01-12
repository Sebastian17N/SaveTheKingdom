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
public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed = 6f;
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.right * 5f;
    }

}
