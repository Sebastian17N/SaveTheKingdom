using UnityEngine;

public class FieldManager : MonoBehaviour
{
	public GameObject Unit;
	public bool IsAssigned = false;

	public void OnMouseOver()
	{
		foreach (var manager in GameObject.FindObjectsOfType<UnitCardManager>())
		{
			manager.Collider = this.GetComponent<FieldManager>();
			manager.IsOverCollider = true;
		}

		if (Unit != null) return;

		Unit = GameObject.FindGameObjectWithTag("Unit");

		if (Unit == null) return;

		Unit.transform.SetParent(this.transform);
		Unit.transform.localPosition = new Vector3(0.04f, 0.3f, -1);
	}
}
