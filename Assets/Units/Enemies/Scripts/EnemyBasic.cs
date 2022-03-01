using System.Collections;
using UnityEngine;

[System.Serializable]   
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBasic : UnitBase
{
    public GameObject MoonStonePrefab;

	private void Start()
	{
        isWalking = true;
        Direction = Vector2.left;

        Team = TeamEnum.Team_2;
    }

	void Update()
    {
        Attack();
        Walking();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    var collider = collision.gameObject;
    //    var bullet = collider.GetComponent<Bullet>();
    //    var unit = collider.GetComponent<UnitBasic>();

    //    if (bullet != null)
    //    {
    //        DecreaseDurability(bullet.Damage);
    //        Destroy(collider);

    //        return;
    //    }

    //    if (unit != null && !unit.IsDragged)
    //    {
    //        isWalking = false;
    //        Target = collision.gameObject;
    //        StartCoroutine(Attack());

    //        return;
    //    }
    //}

    //  public IEnumerator Attack()
    //  {       
    //      if (Target != null)
    //      {
    //          var unitLives = Target.GetComponent<UnitBasic>().DecreaseHealth(AttackDamage);

    //          if (!unitLives)
    //	{
    //              isWalking = true;
    //              yield return new WaitForSeconds(0.5f);
    //	}
    //      }
    //else { isWalking = true; }

    //      yield return new WaitForSeconds(3f);
    //      StartCoroutine(Attack());
    //  }
     
    /// <inheritdoc/>
    public override bool DecreaseDurability(float amount)
    {
        var lastUnitPosition = transform.position;
        var stillExists = base.DecreaseDurability(amount);

        if (!stillExists)
        { 
            FindObjectOfType<GameManager>().NumberOfEnemiesLeft--;
            
            var point = Instantiate(MoonStonePrefab);
            point.transform.position = lastUnitPosition;
        }

        return stillExists;
    }    
}

