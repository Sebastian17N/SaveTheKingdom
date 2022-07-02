using UnityEngine;

[CreateAssetMenu(menuName = "Enemy", fileName = "New Enemy")]
public class ScriptableEnemy : ScriptableObject
{
    public GameObject EnemiesDefault;
    public Sprite Sprite;
    public RuntimeAnimatorController Animator;
    public string Index;
    public float Speed = 2f;
    public float EnemyHealth = 10;
    public int SpawnTime;
    public float AtackDamage;
    public float AttackInterval;
}
