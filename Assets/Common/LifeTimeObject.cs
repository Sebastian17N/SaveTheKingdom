using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimeObject : MonoBehaviour
{
    [SerializeField]
    float LifeTime = 5f;
    void Start()
    {
        Destroy(gameObject, LifeTime);
    }
}
