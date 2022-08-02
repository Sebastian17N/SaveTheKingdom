using TMPro;
using UnityEngine;

namespace Assets.Map.Scripts
{
	public class PointsCounter : MonoBehaviour
	{
		int _points = 0;
		void Start()
		{
			SavePoints();
			RefreshText();        
		}

		public void IncrementPoints()
		{
			_points++;
			SavePoints();
			RefreshText();
		}
		void SavePoints()
		{
			PlayerPrefs.SetInt("current_points", _points);
		}
		void RefreshText()
		{
			GetComponent<TMP_Text>().text = _points.ToString() + " points";
		}
	}
}
