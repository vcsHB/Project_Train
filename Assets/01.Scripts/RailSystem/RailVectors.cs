using System.Collections.Generic;
using UnityEngine;

namespace Project_Train.RailSystem
{
	public static class RailVectors
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
	}
}
