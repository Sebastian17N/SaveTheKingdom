using Assets.Common.Managers;
using Assets.Units.Scripts;
using Assets.Units.Scripts.Enums;
using UnityEngine;

namespace Assets.Units.Enemies.Scripts
{
	[System.Serializable]   
	[RequireComponent(typeof(Rigidbody2D))]
	public class EnemyBasic : UnitBase
	{
		public GameObject MoonStonePrefab;

		private void Start()
		{
			IsWalking = true;
			Direction = Vector2.left;

			Team = TeamEnum.Team2;
		}

		void Update()
		{
			Routine();
		}
         
		/// <inheritdoc/>
		public override bool DecreaseDurability(float amount)
		{
			var lastUnitPosition = transform.position;
			var stillExists = base.DecreaseDurability(amount);

			if (stillExists) 
				return true;

			FindObjectOfType<GameManager>().NumberOfEnemiesLeft--;
            var point = Instantiate(MoonStonePrefab);
			point.transform.position = lastUnitPosition;

			return false;
		}    
	}
}

