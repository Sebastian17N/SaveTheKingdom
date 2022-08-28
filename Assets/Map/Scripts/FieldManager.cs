using Assets.Units.Defenses.Scripts;
using UnityEngine;

namespace Assets.Map.Scripts
{
	public class FieldManager : MonoBehaviour
	{
		public GameObject Unit;
		public GameObject Spell;
		public bool IsAssigned = false;

		public void OnMouseOver()
		{
			if (IsAssigned)
				return;

			UnitOverField();
			SpellOverField();
		}

		public void OnMouseExit()
		{		

			foreach (var manager in FindObjectsOfType<UnitCardManager>())
			{
				manager.Collider = null;
				manager.IsOverCollider = false;
			}

			foreach (var manager in FindObjectsOfType<UseSpell>())
			{
				manager.Collider = null;
				manager.IsOverCollider = false;
			}
		}
		public void UnitOverField()
		{
			foreach (var manager in FindObjectsOfType<UnitCardManager>())
			{
				manager.Collider = GetComponent<FieldManager>();
				manager.IsOverCollider = true;
			}

			if (Unit != null) return;

			Unit = GameObject.FindGameObjectWithTag("Unit");

			if (Unit == null) return;

			Unit.transform.SetParent(transform);
			Unit.transform.localPosition = new Vector3(0.04f, 0.3f, -1);
		}
		public void SpellOverField()
		{
			foreach (var manager in FindObjectsOfType<UseSpell>())
			{
				manager.Collider = GetComponent<FieldManager>();
				manager.IsOverCollider = true;
			}

			if (Spell != null) return;

			Spell = GameObject.FindGameObjectWithTag("Spell");

			if (Spell == null) return;
			
			Spell.transform.SetParent(transform);
			Spell.transform.localPosition = new Vector3(0.04f, 0.3f, -1);

		}
	}
}
