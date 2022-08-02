using Assets.Units.Scripts.Enums;
using UnityEngine;

namespace Assets.Units.Weapons
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(SpriteRenderer))]
	public class Bullet : MonoBehaviour
	{
		public float Damage = 0f;
		public TeamEnum Team;

		public void Configure(BulletType bulletType, float attackDamage, TeamEnum team)
		{
			Damage = attackDamage;
			Team = team;

			GetComponent<SpriteRenderer>().sprite = bulletType.Sprite;
			GetComponent<Rigidbody2D>().velocity = Vector3.right * bulletType.Speed;
		}
	}
}
