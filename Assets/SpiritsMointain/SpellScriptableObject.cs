using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Spell Card", fileName ="New Spell Card")]
public class SpellScriptableObject : ScriptableObject
{
    public Sprite Sprite;
    public int Cooldown;
    public int Damage;
}
