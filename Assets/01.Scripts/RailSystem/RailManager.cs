using System.Collections.Generic;
using UnityEngine;

namespace  Project_Train.RailSystem
{
    // Singleton
    public class RailManager : MonoSingleton<RailManager>
    {
        private Dictionary<Vector3Int, ERailType> _railGrid = new();

        private readonly Vector3Int[] _neighborOffsets =
        {
			Vector3Int.right,
			Vector3Int.left,
			new Vector3Int(0, 1, 1),
			new Vector3Int(0, 1, -1),
			new Vector3Int(0, -1, 1),
			new Vector3Int(0, -1, -1),
			Vector3Int.forward,
			Vector3Int.back
		};


		public ERailType GetRailType(Vector3Int position)
        {
            if (_railGrid.ContainsKey(position) == false) return ERailType.None;

            return _railGrid[position];
        }

        public void UpdateRailAt(Vector3Int position)
        {
            for (int i = 0; i < _neighborOffsets.Length; i++)
            {
                if (_railGrid.ContainsKey(position + _neighborOffsets[i]))
                {
                    // ��ó�� �ִ� ������ ���� Ÿ���� �����ϱ�

                    return;
                }
			}
		}
    }
}
