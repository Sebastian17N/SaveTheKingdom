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
			IsWalking = false;
			Direction = Vector2.right;

			Team = TeamEnum.Team1;
		}

		public void Update()
		{
			Routine();
		}
	}
}
