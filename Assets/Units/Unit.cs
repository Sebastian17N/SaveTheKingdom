using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : ScriptableObject
{
    [SerializeField]
    Sprite Icon;
    [SerializeField]
    int Cost;
    [SerializeField]
    float Cooldown;

}
