using Project_Train.RailSystem;
using System;
using UnityEngine;

namespace Project_Train.Combat.TrainSystem
{
	[System.Serializable]
	public struct WheelDataInfo
	{
		public Transform wheelVisual;
		public Vector3 position;
		private Vector3 _direction;
		public Vector3 Direction
		{
			get { return _direction; }
			set 
			{
				wheelVisual.rotation = Quaternion.LookRotation(value);
				_direction = value; 
			}
		}
	}
	
	public class Train : MonoBehaviour
    {
		[SerializeField] private float _speed = 5f;
		[SerializeField] private WheelDataInfo _wheelA;
		[SerializeField] private WheelDataInfo _wheelB;

		[SerializeField] private Vector3[] _positions;

		private bool _isWent = false;

		public void Go(Rail startRail)
		{

			_isWent = true;
		}

		private void Update()
		{
			transform.position += transform.forward.normalized * Time.deltaTime * _speed;
		}

		private void LateUpdate()
		{
			if (false == _isWent) return;
			VisualMove();
		}

		private void VisualMove()
		{
			var posA = _wheelA.position;
			var posB = _wheelB.position;

			var curPos = Vector3.Lerp(posA, posB, 0.5f) - (RailVectors.railOffset * 0.5f);
			var curDir = (Vector3.Cross(posA, posB).sqrMagnitude > 0 ? (posA - posB) : (posB - posA)).normalized;

			transform.rotation = Quaternion.LookRotation(curDir);
			transform.position = curPos;
		}
	}
}
