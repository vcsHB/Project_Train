using System.Collections.Generic;
using UnityEngine;

namespace Project_Train.RailSystem
{
	public static class RailMath
	{
		public const float RailLength = 4f;
		public static Vector3 railOffset = Vector3.up;
		private const float CurveCenterPos = 0.35f;

		private static readonly Dictionary<ERailType, (Vector3, Vector3, Vector3)> _railpoints = new()
		{
			{ ERailType.None, (Vector3.zero, Vector3.zero, Vector3.zero) },

			{ ERailType.Straight_NS, (new Vector3(0, 0, RailLength * 0.5f), Vector3.zero, new Vector3(0, 0, RailLength * -0.5f))},
			{ ERailType.Straight_EW, (new Vector3(RailLength * 0.5f, 0, 0), Vector3.zero, new Vector3(RailLength * -0.5f, 0, 0))},

			{ ERailType.Curve_NE, (new Vector3(0, 0, RailLength * 0.625f),  new Vector3(CurveCenterPos, 0, CurveCenterPos), new Vector3(RailLength * 0.625f, 0, 0))},
			{ ERailType.Curve_ES, (new Vector3(RailLength * 0.625f, 0, 0),  new Vector3(CurveCenterPos, 0, -CurveCenterPos), new Vector3(0, 0, RailLength * -0.625f))},
			{ ERailType.Curve_SW, (new Vector3(0, 0, RailLength * -0.625f), new Vector3(-CurveCenterPos, 0, -CurveCenterPos), new Vector3(RailLength * -0.625f, 0, 0))},
			{ ERailType.Curve_WN, (new Vector3(RailLength * -0.625f, 0, 0), new Vector3(-CurveCenterPos, 0, CurveCenterPos), new Vector3(0, 0, RailLength * 0.625f))},

			{ ERailType.Ascending_N, (new Vector3(0, 0, RailLength * -0.5f), new Vector3(0, 0.5f, 0), new Vector3(0, 1f, RailLength * 0.5f))},
			{ ERailType.Ascending_E, (new Vector3(RailLength * -0.5f, 0, 0), new Vector3(0, 0.5f, 0), new Vector3(RailLength * 0.5f, 1f, 0))},
			{ ERailType.Ascending_S, (new Vector3(0, 0, RailLength * 0.5f),  new Vector3(0, 0.5f, 0), new Vector3(0, 1f, RailLength * -0.5f))},
			{ ERailType.Ascending_W, (new Vector3(RailLength * 0.5f, 0, 0),  new Vector3(0, 0.5f, 0), new Vector3(RailLength * -0.5f, 1f, 0))},
		};

		public static void GetPoints(ERailType railType, out Vector3 p0, out Vector3 p1, out Vector3 p2)
		{
			var points = _railpoints[railType];
			p0 = points.Item1;
			p1 = points.Item2;
			p2 = points.Item3;
		}

		public static Vector3[] GetPoints(ERailType railType)
		{
			Vector3[] result = new Vector3[3];
			var points = _railpoints[railType];
			result[0] = points.Item1;
			result[1] = points.Item2;
			result[2] = points.Item3;

			return result;
		}

		public static Vector3 GetQuadraticBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
		{
			t = Mathf.Clamp01(t);
			float oneMinusT = 1f - t;
			return oneMinusT * oneMinusT * p0 +
				   2f * oneMinusT * t * p1 +
				   t * t * p2;
		}

		public static Vector3 GetQuadraticBezierTangent(Vector3 p0, Vector3 p1, Vector3 p2, float t)
		{
			return (2f * (1f - t) * (p1 - p0) + 2f * t * (p2 - p1)).normalized;
		}

		public static float GetTForQuadraticBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p)
		{
			Vector3 A = p0 - 2 * p1 + p2;
			Vector3 B = 2 * (p1 - p0);
			Vector3 C = p0 - p;

			if (A.sqrMagnitude < 0.0001f)
			{
				if (B.sqrMagnitude < 0.0001f) return 0.0f;
				float t = -Vector3.Dot(B, C) / B.sqrMagnitude;
				return t;
			}

			float a = Vector3.Dot(A, A);
			float b = Vector3.Dot(A, B);
			float c = Vector3.Dot(A, C);

			float discriminant = b * b - 4 * a * c;
			if (discriminant < 0)
			{
				return -1.0f;
			}

			float sqrtDiscriminant = Mathf.Sqrt(discriminant);
			float t1 = (-b + sqrtDiscriminant) / (2 * a);
			float t2 = (-b - sqrtDiscriminant) / (2 * a);

			if (t1 >= 0 && t1 <= 1) return t1;
			if (t2 >= 0 && t2 <= 1) return t2;

			return -1.0f;
		}

		public static Vector3 SnapToCardinal(Vector3 direction)
		{
			Vector3[] cardinalDirections =
			{
				Vector3.right,
				Vector3.left,
				Vector3.forward,
				Vector3.back
			};

			Vector3 bestDirection = cardinalDirections[0];
			float maxDot = Vector3.Dot(direction, bestDirection);

			for (int i = 1; i < cardinalDirections.Length; i++)
			{
				float dot = Vector3.Dot(direction, cardinalDirections[i]);
				if (dot > maxDot)
				{
					maxDot = dot;
					bestDirection = cardinalDirections[i];
				}
			}

			return bestDirection;
		}

		public static Vector3 GetRoundedPosition(Vector3 position)
		{
			position.x = Mathf.Round(position.x * 10) / 10;
			position.y = Mathf.Round(position.y * 10) / 10;
			position.z = Mathf.Round(position.z * 10) / 10;

			return position;
		}
	}
}
