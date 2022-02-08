using UnityEngine;

public class FieldManager : MonoBehaviour
{
	public GameObject Unit;
	public bool IsAssigned = false;

	public void OnMouseOver()
	{
		if (IsAssigned)
			return;

		foreach (var manager in FindObjectsOfType<UnitCardManager>())
		{
			manager.Collider = GetComponent<FieldManager>();
			manager.IsOverCollider = true;
		}

		if (Unit != null) return;

		Unit = GameObject.FindGameObjectWithTag("Unit");

		if (Unit == null) return;

		Unit.transform.SetParent(this.transform);
		Unit.transform.localPosition = new Vector3(0.04f, 0.3f, -1);
	}

	public void OnMouseExit()
	{
		foreach (var manager in FindObjectsOfType<UnitCardManager>())
		{
			manager.Collider = null;
			manager.IsOverCollider = false;
		}
	}
}
