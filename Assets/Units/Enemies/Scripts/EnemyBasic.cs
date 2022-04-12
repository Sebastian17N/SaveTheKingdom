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

