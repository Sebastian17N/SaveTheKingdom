using Assets.Units.Defenses.Scripts;
using Assets.Units.Enemies.Scripts;
using Assets.Units.Scripts.Enums;
using Assets.Units.Weapons;
using UnityEngine;

namespace Assets.Units.Scripts
{
	public abstract class UnitBase : MonoBehaviour, IDecreaseDurabilityOwner
	{
		// Default statuses
		protected bool isWalking;
		protected bool isAttacking;

		// Prefabs
		public GameObject BulletPrefab;
		public BulletType BulletType;

		// Fighting attributes
		public float Speed;
		public float Health;
    
		public float AttackSpeed;
		public float AttackDamage;

		#region Enviroment attributes
		protected TeamEnum Team;
		public bool IsDragged { get; set; } = false;
		public bool IsRange { get; set; } = false;

		/// <summary>
		/// Define in which direction units are walking and attacking.
		/// </summary>
		public Vector2 Direction;

		private float _lastShootTime = 0f;

		private IDecreaseDurabilityOwner _enemy;
		public GameObject _bullet;

		#endregion Enviroment attributes

		protected void Routine()
		{
			if (IsDragged)
				return;

			// Got hit.
			if (_bullet != null && _bullet.GetComponent<Bullet>() != null)
			{
				DecreaseDurability(_bullet.GetComponent<Bullet>().Damage);
				Destroy(_bullet);
			}

			if (Time.timeSinceLevelLoad - _lastShootTime < AttackSpeed)
				return;

			// Check if there is enemy next to it. If not, go for a walk.
			if (((MonoBehaviour)_enemy) == null && Speed > 0)
			{
				Walk();
				return;
			}
			else
			{
				StopWalking();
			}

			Attack();
		}

		public void OnTriggerEnter2D(Collider2D collision)
		{
			var collidedObject = collision.gameObject;
			var unitBase = collidedObject.GetComponent<UnitBase>();

			// Do not collide with object which are not on a field.
			if (unitBase != null && unitBase.IsDragged 
			    || IsDragged)
				return;

			if (_enemy == null && unitBase != null)
			{
				IDecreaseDurabilityOwner enemy = 
					Team == TeamEnum.Team_1 ?
						collidedObject.GetComponent<EnemyBasic>() :
						collidedObject.GetComponent<UnitBasic>();

				if (enemy != null && (enemy as UnitBase).Team != Team)
					_enemy = enemy;
			}

			if (_bullet == null 
			    && collidedObject.GetComponent<Bullet>() != null
			    && collidedObject.GetComponent<Bullet>()?.Team != Team)
			{
				_bullet = collidedObject;
			}
		}

		private void Walk()
		{
			isWalking = false;
			GetComponent<Rigidbody2D>().velocity = Direction * Speed;
		}

		private void StopWalking()
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		}

		private void Attack()
		{
			_lastShootTime = Time.timeSinceLevelLoad;

			// Range attack.
			if (BulletPrefab != null)
			{
				ShootBullet();
				return;
			}

			// Mele attack. Remove reference if he was defeated.
			if (!_enemy?.DecreaseDurability(AttackDamage) ?? false)
				_enemy = null;
			else
				isWalking = false;
		}

		private void ShootBullet()
		{
			var bullet = Instantiate(BulletPrefab);
			bullet.transform.position = transform.position + new Vector3(Direction.x, Direction.y) * 1.5f + Vector3.down * 0.5f;

			bullet.GetComponent<Bullet>().Configure(BulletType, AttackDamage, Team);
			bullet.transform.SetParent(transform.parent);
		}

		public virtual bool DecreaseDurability(float amount)
		{
			Health -= amount;

			if (Health <= 0)
			{
				Destroy(gameObject);
				return false;
			}

			return true;
		}
	}
}
