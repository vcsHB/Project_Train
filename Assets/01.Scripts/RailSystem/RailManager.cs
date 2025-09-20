using System.Collections.Generic;
using UnityEngine;

namespace  Project_Train.RailSystem
{
    // Singleton
    public class RailManager : MonoSingleton<RailManager>
    {
        private Dictionary<Vector3, List<Rail>> _railGrid = new();

		public List<Rail> GetRails(Vector3 position)
        {
			position = RailMath.GetRoundedPosition(position);

			if (_railGrid.ContainsKey(position) == false) return null;

            return _railGrid[position];
        }

		public Rail GetRail(Vector3 position, Rail ignoreRail)
		{
			position = RailMath.GetRoundedPosition(position);

			for (int i = 0; i < _railGrid[position].Count; i++)
				if (_railGrid[position][i] != ignoreRail)
					return _railGrid[position][i];

			return null;
		}

		public void AddRail(Rail rail, Vector3 position)
		{
			position = RailMath.GetRoundedPosition(position);

			if (!_railGrid.ContainsKey(position))
				_railGrid.Add(position, new List<Rail>());

			_railGrid[position].Add(rail);
		}
	}
}
