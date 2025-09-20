using Project_Train.RailSystem;
using UnityEngine;

namespace Project_Train.Combat.TrainSystem
{
	public abstract class CarBase : MonoBehaviour
	{
		[Header("TrainArrays")]
		public CarBase headCar;
		public CarBase frontCar;
		public CarBase backCar;
		public bool IsHeadCar => null == frontCar;

		public abstract float TargetSpeed { get; protected set; }
		public float SpeedStack { get; set; }
		public bool IsRunning { get; private set; }
		public Rail CurrentRail => _wheelB.CurrentRail;

		[Header("Wheels")]
		[SerializeField] private Wheel _wheelA;
		[SerializeField] private Wheel _wheelB;

		private bool _isInitialized = false;

		public void Initialize(Rail startRail)
		{
			_wheelA.Initialize(startRail, transform);
			_wheelB.Initialize(startRail, transform);

			_isInitialized = true;
		}

		protected virtual void Update()
		{
			if (false == _isInitialized) return;

			if (IsHeadCar && this != headCar)
			{
				SpeedStack = 0;
				SetHeadCar(this);
			}

			SetupFinalSpeed();
		}

		private void LateUpdate()
		{
			if (false == _isInitialized) return;

			VisualMove();
		}

		public virtual void SetHeadCar(CarBase headCar)
		{
			this.headCar = headCar;
			if (null != backCar)
				backCar.SetHeadCar(headCar);
		}

		// set wheel's speed to final speed 
		private void SetupFinalSpeed()
		{
			if (null == headCar) return;
			IsRunning = headCar.SpeedStack > 0;
			var finalSpeed = IsRunning ? headCar.SpeedStack : 0.1f;
			_wheelA.speed = _wheelB.speed = finalSpeed;
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
