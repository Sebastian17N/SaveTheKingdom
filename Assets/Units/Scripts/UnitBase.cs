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
		if (((MonoBehaviour)_enemy) == null)
        {
            isWalking = true;
            return;
        }

        // Attack enemy. Remove reference if he was defeated.
        if (!_enemy.DecreaseDurability(AttackDamage))
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
            && collidedObject.GetComponent<Bullet>()?.Team != Team)
        {
            _bullet = collidedObject;
        }
    }

    private void ShootBullet()
    {
        var bullet = Instantiate(BulletPrefab);
        bullet.transform.position = transform.position + new Vector3(Direction.x, Direction.y) * 1.5f + Vector3.down * 0.5f;

        bullet.GetComponent<Bullet>().Configure(BulletType, AttackDamage, Team);
        bullet.transform.SetParent(transform);

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
