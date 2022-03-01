using UnityEngine;

[CreateAssetMenu(menuName = "Units/Unit Card", fileName = "New Unit Card")]
public class UnitScriptableObject : ScriptableObject
{
    public Texture Icon;
    public Sprite Sprite;
    
    public Sprite Bullet;
    public BulletType BulletType;
    public RuntimeAnimatorController Animator;

    public int Cost;
    public bool IsRange;
    public float AttackSpeed;
}
