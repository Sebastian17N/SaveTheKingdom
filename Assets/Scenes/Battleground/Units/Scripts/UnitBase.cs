using Assets.Units.Defenses.Scripts;
using Assets.Units.Enemies.Scripts;
using Assets.Units.Scripts.Enums;
using Assets.Units.Weapons;
using UnityEngine;

namespace Assets.Units.Scripts
{
	public abstract class UnitBase : MonoBehaviour, IDecreaseDurabilityOwner
	{
		// Prefabs
		public GameObject BulletPrefab;
		public BulletType BulletType;

		// Fighting attributes
		public float Speed;
		public float Health;
    
		public float AttackSpeed;
		public float AttackDamage;

		private bool spawned = false;
		private Animator _animator;

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
		public GameObject Bullet;
		public LayerMask enemiesLayer;

		#endregion Enviroment attributes

		public void Awake()
		{
			_animator = GetComponent<Animator>();
		}
		
        protected void Routine()
		{
			if (IsDragged)
				return;

			if (!spawned)
			{
				Walk();
				spawned = true;
			}

			// Got hit.
			if (Bullet != null && Bullet.GetComponent<Bullet>() != null)
			{
				DecreaseDurability(Bullet.GetComponent<Bullet>().Damage);
				Destroy(Bullet);
			}

			if (Time.timeSinceLevelLoad - _lastShootTime < AttackSpeed)
				return;
			
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
					Team == TeamEnum.Team1 ?
						collidedObject.GetComponent<EnemyBasic>() :
						collidedObject.GetComponent<UnitBasic>();

				if (enemy != null && ((UnitBase)enemy).Team != Team)
				{
					_enemy = enemy;

					StopWalking();
				}
			}

			if (Bullet == null 
			    && collidedObject.GetComponent<Bullet>() != null
			    && collidedObject.GetComponent<Bullet>()?.Team != Team)
			{
				Bullet = collidedObject;
			}
		}

		private void Walk()
		{
			transform.GetComponent<Rigidbody2D>().velocity = Direction * Speed;
			_animator.SetTrigger("NoEnemies");
		}

		private void StopWalking()
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			_animator.SetTrigger("Attack");
		}

		private void Attack()
		{
			_lastShootTime = Time.timeSinceLevelLoad;
			
			RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 100f, enemiesLayer);

			// Unit atak only if eniemies is in front of her
			// Range attack.
			if (hit && BulletPrefab != null)
            {
				ShootBullet();
			}

			if (!_enemy?.DecreaseDurability(AttackDamage) ?? false)
            {
				_enemy = null;
				Walk();
            }
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
