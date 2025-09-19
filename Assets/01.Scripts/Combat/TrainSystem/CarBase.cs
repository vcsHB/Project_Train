using Project_Train.RailSystem;
using UnityEngine;

namespace Project_Train.Combat.TrainSystem
{
	public abstract class CarBase : MonoBehaviour
	{
		public CarBase headCar;
		public CarBase frontCar;
		public CarBase backCar;
		public bool IsHeadCar => null == frontCar;

		public abstract float TargetSpeed { get; protected set; }
		public float SpeedStack { get; set; }
		public bool IsRunning { get; private set; }

		/// <summary>
		/// The standard is back wheel
		/// </summary>
		public Rail currentRail;

		[SerializeField] private Wheel _wheelA;
		[SerializeField] private Wheel _wheelB;

		protected virtual void Update()
		{
			if (true == IsHeadCar && null == headCar)
			{
				SpeedStack = 0;
				SetHeadCar(this);
				SetupFinalSpeed();
			}
		}

		private void LateUpdate()
		{
			VisualMove();
		}

		public virtual void SetHeadCar(CarBase headCar)
		{
			this.headCar = headCar;
			if (null != backCar)
				backCar.SetHeadCar(headCar);
		}

		private void SetupFinalSpeed()
		{
			if (null == headCar) return;
			IsRunning = SpeedStack > 0;
			SetIsRunning(this);
			var finalSpeed = IsRunning ? SpeedStack : 0.1f;
			_wheelA.speed = _wheelB.speed = finalSpeed;
		}

		private void SetIsRunning(CarBase headCar)
		{
			if (headCar != this)
				this.IsRunning = headCar.IsRunning;

			if (null != backCar)
				backCar.SetIsRunning(headCar);
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
