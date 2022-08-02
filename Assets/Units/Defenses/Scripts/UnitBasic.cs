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
			isWalking = false;
			Direction = Vector2.right;

			Team = TeamEnum.Team_1;
		}

		public void Update()
		{
			Routine();
		}
	}
}
