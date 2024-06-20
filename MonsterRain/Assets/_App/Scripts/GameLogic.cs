using UnityEngine;
namespace _App.Scripts
{
	public static class GameLogic
	{
		public static float CalculateDistance(Vector2 vector1, Vector2 vector2)
		{
			var x = vector1.x - vector2.x;
			var y = vector1.y - vector2.y;
			return x * x + y * y;
		}
	}
}