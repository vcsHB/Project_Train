using Project_Train.RailSystem;
using System;
using UnityEngine;

namespace Project_Train.Combat.TrainSystem
{
	//[Serializable]
	//public struct CartDataInfo
	//{
	//	public Cart cart;
	//	public Vector3 
	//}

	public class Train : MonoBehaviour
	{
		[SerializeField] private float _speed = 5f;
		[SerializeField] private Wheel _wheelA;
		[SerializeField] private Wheel _wheelB;

		private void Update()
		{
			transform.position += transform.forward.normalized * Time.deltaTime * _speed;
		}

		private void LateUpdate()
		{
			VisualMove();
		}

		private void VisualMove()
		{
			var posA = _wheelA.transform.position;
			var posB = _wheelB.transform.position;

			var curPos = Vector3.Lerp(posA, posB, 0.5f) - (RailMath.railOffset * 0.5f);
			var curDir = (Vector3.Cross(posA, posB).sqrMagnitude > 0 ? (posA - posB) : (posB - posA)).normalized;

			transform.rotation = Quaternion.LookRotation(curDir);
			transform.position = curPos;
		}
	}
}
