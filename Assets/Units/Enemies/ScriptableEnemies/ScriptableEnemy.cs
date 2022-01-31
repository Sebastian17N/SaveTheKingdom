using UnityEngine;

[CreateAssetMenu(menuName = "Enemy", fileName = "New Enemy")]
public class ScriptableEnemy : ScriptableObject
{
    public GameObject EnemiesDefault;
    public Sprite Sprite;
    public float Speed = 2f;
    public float EnemyHealth = 10;
    public int SpawnTime;
    public float AtackDamage;
    public float AtackInterval;
}
