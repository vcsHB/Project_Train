using UnityEngine;

namespace Project_Train.Combat.TrainSystem
{
    public class Train : MonoBehaviour
    {
		[SerializeField] private float _speed = 5f;

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
			
		}
	}
}
