using System;
using System.Collections.Generic;
using UnityEngine;

namespace  Project_Train.RailSystem
{
    // Singleton
    public class RailManager : MonoSingleton<RailManager>
    {
        private Dictionary<Vector3Int, ERailType> _railGrid = new();

  //      private readonly Vector3Int[] _neighborOffsets =
  //      {
		//	Vector3Int.right,
		//	Vector3Int.left,
		//	Vector3Int.forward,
		//	Vector3Int.back,
		//	new Vector3Int(0, 1, 1),
		//	new Vector3Int(0, 1, -1),
		//	new Vector3Int(0, -1, 1),
		//	new Vector3Int(0, -1, -1),
		//	new Vector3Int(1, 1, 0),
		//	new Vector3Int(-1, 1, 0),
		//	new Vector3Int(1, -1, 0),
		//	new Vector3Int(-1, -1, 0),
		//};


		public ERailType GetRailType(Vector3Int position)
        {
            if (_railGrid.ContainsKey(position) == false) return ERailType.None;

            return _railGrid[position];
        }

		public void AddRail(ERailType railType, Vector3Int position)
		{
			if (_railGrid.ContainsKey(position)) return;
			_railGrid.Add(position, railType);
		}
	}
}
