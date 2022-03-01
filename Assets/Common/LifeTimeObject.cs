using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimeObject : MonoBehaviour
{
    [SerializeField]
    public float LifeTime = 5f;
    public void Start()
    {
        Destroy(gameObject, LifeTime);
    }
}
