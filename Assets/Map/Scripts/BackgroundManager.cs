using UnityEngine;

namespace Assets.Map.Scripts
{
	public class BackgroundManager : MonoBehaviour
	{
		public GameObject FieldPrefab;
		public int BoardWidth;
		public int BoardHeight;
		public Vector2 FieldLocation = Vector2.one;
		private Vector3 TargetPosition;
		//public int X { get; private set; }
		//public int Y { get; private set; }

		void Start()
		{
			CreateFieldBoard();
		}        
        private void CreateFieldBoard()
		{
			for (var x = 0; x < BoardWidth; x++)
			{
				for (var y = 0; y < BoardHeight; y++)
				{
					CreateField(x, y);
				}
			}
		}

		private void CreateField(int x, int y)
		{
			var field = Instantiate(FieldPrefab, transform);
			field.transform.localPosition += new Vector3(x * FieldLocation.x, y * FieldLocation.y, 0);
			field.GetComponent<FieldManager>().X = x;
			field.GetComponent<FieldManager>().Y = y;
			//TargetPosition = GetFieldPosition(x, y);
		}
				
		//public Vector2 GetFieldPosition(int x, int y)
		//{
		//	var basePosition = new Vector2(
		//		x - BoardWidth / 2 + 0.5f,
		//		y - BoardHeight / 2 + 0.5f);

		//	return basePosition;
		//}
		//public bool IsNeighbour(BackgroundManager field)
		//{
		//	if (Mathf.Abs(X - field.X) > 1f)
		//		return false;
		//	if (Mathf.Abs(Y - field.Y) > 1f)
		//		return false;

		//	return true;
		//}
	}
}