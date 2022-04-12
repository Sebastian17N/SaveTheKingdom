using UnityEngine;

[CreateAssetMenu(menuName = "Units/Unit Card", fileName = "New Unit Card")]
public class UnitScriptableObject : ScriptableObject
{
    [Header("Visuals")]
    public Sprite Sprite;
    public RuntimeAnimatorController Animator;

    [Header("Statistics")]
    public int Cost;
    public int Health;
    public bool IsRange => BulletType?.Sprite != null;
    public BulletType BulletType;
    
    public float AttackSpeed;
    public float AttackDamage;

    public float Cooldown;
}
