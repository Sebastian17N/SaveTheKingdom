using UnityEngine;

namespace Assets.Units.Enemies.Scripts
{
	[CreateAssetMenu(fileName = "Enemy_", menuName = "Scriptable Objects/Enemy")]
	public class ScriptableEnemy : ScriptableObject
	{
		public GameObject EnemiesDefault;
		public Sprite Sprite;
		public RuntimeAnimatorController Animator;
		public string Index;
		public float Speed = 2f;
		public float EnemyHealth = 1000;
		public int SpawnTime;
		public float AtackDamage;
		public float AttackInterval;
	}
}
