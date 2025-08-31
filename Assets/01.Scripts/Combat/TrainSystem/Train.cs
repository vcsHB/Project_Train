using Project_Train.RailSystem;
using UnityEngine;

namespace Project_Train.Combat.TrainSystem
{
    public class Train : MonoBehaviour
    {
		[SerializeField] private float _speed = 5f;
		[SerializeField] private Cart _cartA;
		[SerializeField] private Cart _cartB;

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
			var posA = _cartA.transform.position;
			var posB = _cartB.transform.position;

			var curPos = Vector3.Lerp(posA, posB, 0.5f) - (RailVectors.railOffset * 0.5f);
			var curDir = (Vector3.Cross(posA, posB).sqrMagnitude > 0 ? (posA - posB) : (posB - posA)).normalized;

			transform.rotation = Quaternion.LookRotation(curDir);
			transform.position = curPos;
		}
	}
}
