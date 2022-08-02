using UnityEngine;

namespace Assets.Scenes.CampaignMap.Scripts
{
	public class CameraMovement : MonoBehaviour
	{
		[SerializeField] public Camera CameraMain;
		[SerializeField] public float MinCamSize;
		[SerializeField] public float MaxCamSize;
		private Vector3 _frameOrigin;

		public SpriteRenderer MapRenderer;
		private float _mapMinX, _mapMaxX, _mapMinY, _mapMaxY;

		private void Awake()
		{
			_mapMinX = MapRenderer.transform.position.x - MapRenderer.bounds.size.x / 2;
			_mapMaxX = MapRenderer.transform.position.x + MapRenderer.bounds.size.x / 2;

			_mapMinY = MapRenderer.transform.position.y - MapRenderer.bounds.size.y / 2;
			_mapMaxY = MapRenderer.transform.position.y + MapRenderer.bounds.size.y / 2;
		}

		void Update()
		{
			PanCamera();
			Zoom(Input.GetAxis("Mouse ScrollWheel"));
		}

		private void PanCamera()
		{
			var cameraOnMousePosition =
				CameraMain.ScreenToWorldPoint(Input.mousePosition);

			if (Input.GetMouseButtonDown(0))
				_frameOrigin = cameraOnMousePosition;

			if (Input.touchCount == 2)
			{
				var touchZero = Input.GetTouch(0);
				var touchOne = Input.GetTouch(1);

				var touchZeroPrevPosition = touchZero.position - touchZero.deltaPosition;
				var touchOnePrevPosition = touchOne.position - touchOne.deltaPosition;

				var prevMagnitude = (touchZeroPrevPosition - touchOnePrevPosition).magnitude;
				var currentMagnitude = (touchZero.position - touchOne.position).magnitude;

				var difference = currentMagnitude - prevMagnitude;

				Zoom(difference * 0.01f);
			}
			else if (Input.GetMouseButton(0))
			{
				var direction = _frameOrigin - cameraOnMousePosition;

				CameraMain.transform.position =
					ClampCamera(CameraMain.transform.position + direction);
			}
		}

		private void Zoom(float increment)
		{
			Camera.main.orthographicSize = Mathf.Clamp
				(Camera.main.orthographicSize - increment, MinCamSize, MaxCamSize);
		}

		private Vector3 ClampCamera(Vector3 targetPosition)
		{
			var camHeight = CameraMain.orthographicSize;
			var camWidth = CameraMain.orthographicSize * CameraMain.aspect;

			var minX = _mapMinX + camWidth;
			var maxX = _mapMaxX - camWidth;
			var minY = _mapMinY + camHeight;
			var maxY = _mapMaxY - camHeight;

			var newX = Mathf.Clamp(targetPosition.x, minX, maxX);
			var newY = Mathf.Clamp(targetPosition.y, minY, maxY);

			return new Vector3(newX, newY, targetPosition.z);
		}
	}
}