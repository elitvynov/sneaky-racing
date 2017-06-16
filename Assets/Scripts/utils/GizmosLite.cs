namespace sneakyRacing
{
	using UnityEngine;

	public class GizmosLite
	{
		public static Color color = Color.white;

		public static void drawLineArrowXY(Vector3 from, Vector3 to)
		{
			to -= from;

			drawRayArrowXY(from, to, 0.35f, 25.0f);
		}

		public static void drawLineArrowXY(Vector3 from, Vector3 to, float arrowLength, float arrowAngle)
		{
			to -= from;

			drawRayArrowXY(from, to, arrowLength, arrowAngle);
		}

		public static void drawLineArrowYZ(Vector3 from, Vector3 to)
		{
			to -= from;
			
			drawRayArrowYZ(from, to, 0.35f, 25.0f);
		}

		public static void drawLineArrowYZ(Vector3 from, Vector3 to, float arrowLength, float arrowAngle)
		{
			to -= from;

			drawRayArrowYZ(from, to, arrowLength, arrowAngle);
		}

		public static void drawRayArrowYZ(Vector3 pos, Vector3 direction, float arrowLength, float arrowAngle)
		{
			//Console.trace("direction = " + direction + "direction.sqrMagnitude = " + direction.sqrMagnitude);

			if (direction.sqrMagnitude > 0.005f)
			{
				Vector3 right = Quaternion.LookRotation(direction, Vector3.left) * Quaternion.Euler(0, 180 + arrowAngle, 0) * Vector3.forward;
				Vector3 left = Quaternion.LookRotation(direction, Vector3.left) * Quaternion.Euler(0, 180 - arrowAngle, 0) * Vector3.forward;

				Gizmos.color = color;
				Gizmos.DrawRay(pos, direction);
				Gizmos.DrawRay(pos + direction, right * arrowLength); 
				Gizmos.DrawRay(pos + direction, left * arrowLength);
			}
		}
		
		public static void drawArrowXY(Vector3 from, Vector3 to)
		{
			to -= from;

			drawRayArrowXY(from, to, 0.2f, 20.0f);
		}
		
		public static void drawRayArrowXY(Vector3 pos, Vector3 direction, float arrowLength, float arrowAngle)
		{
			if (direction.sqrMagnitude > 0.005f)
			{
				Vector3 right = Quaternion.LookRotation(direction, Vector3.left) * Quaternion.Euler(180 + arrowAngle, 0, 0) * Vector3.forward;
				Vector3 left = Quaternion.LookRotation(direction, Vector3.left) * Quaternion.Euler(180 - arrowAngle, 0, 0) * Vector3.forward;

				Gizmos.color = color;
				Gizmos.DrawRay(pos, direction);
				Gizmos.DrawRay(pos + direction, right * arrowLength);
				Gizmos.DrawRay(pos + direction, left * arrowLength);
			}
		}
	}
}