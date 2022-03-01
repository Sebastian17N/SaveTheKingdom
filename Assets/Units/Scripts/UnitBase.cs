using UnityEngine;

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
    public float AttackSpeed;
    public float Health;

    #region Enviroment attributes
    protected TeamEnum Team;
    public bool IsDragged { get; set; } = false;

    /// <summary>
    /// Define in which direction units are walking and attacking.
    /// </summary>
    public Vector2 Direction;

    private float _lastShootTime = 0f;

    private IDecreaseDurabilityOwner _enemy;
    public GameObject _bullet;

    #endregion Enviroment attributes

    protected void Walking()
    {
        if (isWalking)
        {
            isWalking = false;
            GetComponent<Rigidbody2D>().velocity = Direction * Speed;
        }
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    protected void Attack()
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

		// Range attack.
		if (BulletPrefab != null)
		{
			ShootBullet();
			return;
		}

		// Check if there is enemy next to it. If not, go for a walk.
		if (_enemy == null)
        {
            isWalking = true;
            return;
        }

        // Attack enemy. Remove reference if he was defeated.
        if (!_enemy.DecreaseDurability(1))
            _enemy = null;
        else
        {
            isWalking = false;
            _lastShootTime = Time.timeSinceLevelLoad;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var collidedObject = collision.gameObject;

        if (_enemy == null && collidedObject.GetComponent<UnitBase>())
        {
            IDecreaseDurabilityOwner enemy = 
                Team == TeamEnum.Team_1 ?
                    collidedObject.GetComponent<EnemyBasic>() :
                    collidedObject.GetComponent<UnitBasic>();

            if (enemy != null && (enemy as UnitBase).Team != Team)
                _enemy = enemy;
        }

        if (_bullet == null)
        {
            var bullet = collidedObject;

            // TODO: Check which side is bullet, to prevent friendly fire.
            if (bullet.GetComponent<Bullet>() != null)
                _bullet = bullet;
        }
    }

    private void ShootBullet()
    {
        var bullet = Instantiate(BulletPrefab);
        bullet.transform.position = transform.position + new Vector3(Direction.x, Direction.y) * 1.5f + Vector3.down * 0.5f;

        bullet.GetComponent<Bullet>().Configure(BulletType);
        bullet.transform.SetParent(this.transform);

        _lastShootTime = Time.timeSinceLevelLoad;
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
