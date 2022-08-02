using System;
using UnityEngine;

namespace Assets.Scenes.FightSummary.Scripts
{
	public class MathParabola : MonoBehaviour
	{
		public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
		{
			float ParabolaEquation(float x) => -4 * height * x * x + 4 * height * x;

			var mid = Vector2.Lerp(start, end, t);

			return new Vector2(mid.x, ParabolaEquation(t) + Mathf.Lerp(start.y, end.y, t));
		}
	}
}
