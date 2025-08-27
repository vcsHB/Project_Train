using UnityEngine;

namespace Project_Train.Combat.TrainSystem
{
    public class Train : MonoBehaviour
    {
		[SerializeField] private float _speed = 5f;
        [SerializeField] private Transform _wheel1;
        [SerializeField] private Transform _wheel2;

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
			if (_wheel1 == null || _wheel2 == null) return;

			var rotation = transform.localEulerAngles;

			_wheel1.transform.localEulerAngles = rotation;
			_wheel2.transform.localEulerAngles = new Vector3(rotation.x, -rotation.y, rotation.z);
		}
	}
}
