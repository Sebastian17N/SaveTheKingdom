using Assets.Common.Managers;
using Assets.Units.Scripts;
using Assets.Units.Scripts.Enums;
using UnityEngine;

namespace Assets.Units.Defenses.Scripts
{
	public class UnitBasic : UnitBase, IIsDraggedOwner
	{
		private void Start()
		{
			IsDragged = true;
			Direction = Vector2.right;

			Team = TeamEnum.Team1;
		}

		public void Update()
		{
			Routine();
		}

		public override bool DecreaseDurability(float amount)
		{
			var gameManager = FindObjectOfType<GameManager>();
			gameManager.DamageDealtOnCurrentLevel += amount;

			return base.DecreaseDurability(amount);
		}
	}
}
