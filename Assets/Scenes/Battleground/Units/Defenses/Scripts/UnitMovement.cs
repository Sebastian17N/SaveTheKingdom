using UnityEngine;

namespace Assets.Units.Defenses.Scripts
{
	public class UnitMovement : MonoBehaviour
	{
		[SerializeField]
		Vector2 _movementArea;

		Camera _camera;
		void Start()
		{
			_camera = FindObjectOfType<Camera>();
		}

		private void OnDrawGizmos()
		{
			Gizmos.DrawWireCube(Vector3.zero, _movementArea * 2f);

		}
		void Update()
		{ 
			Vector2 targetPosition;
			if (!Input.GetMouseButton(0))
				return;

			targetPosition = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
			targetPosition.x = Mathf.Clamp(targetPosition.x, -_movementArea.x, _movementArea.x);
			targetPosition.y = Mathf.Clamp(targetPosition.y, -_movementArea.y, _movementArea.y);

			transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
		}
	}
}
