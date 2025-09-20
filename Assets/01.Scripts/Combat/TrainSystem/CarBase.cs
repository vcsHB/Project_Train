using Project_Train.Combat.CasterSystem.HitBody;
using Project_Train.RailSystem;
using UnityEngine;

namespace Project_Train.Combat.TrainSystem
{
	public abstract class CarBase : MonoBehaviour
	{
		private CarBase _headCar;
		private CarBase _frontCar;
		private CarBase _backCar;
		private bool _isHandlingCompositionChange;

		[Header("TrainArrays")]
		public CarBase headCar
		{
			get => _headCar;
			set
			{
				if (_headCar == value) return;
				_headCar = value;
				HandleTrainCompositionChange();
			}
		}
		public CarBase frontCar 
		{
			get => _frontCar;
			set
			{
				if (_frontCar == value) return;
				_frontCar = value;
				HandleTrainCompositionChange();
			}
		}
		public CarBase backCar
		{
			get => _backCar;
			set
			{
				if (_backCar == value) return;
				_backCar = value;
				HandleTrainCompositionChange();
			}
		}
		public bool IsHeadCar => null == frontCar;

		public abstract float TargetSpeed { get; protected set; }
		public float SpeedStack { get; set; }
		public bool IsRunning { get; private set; }
		public Rail CurrentRail => _wheelB.CurrentRail;

		[Header("Wheels")]
		[SerializeField] private Wheel _wheelA;
		[SerializeField] private Wheel _wheelB;

		private bool _isInitialized = false;

		private Health _health;
		private TrainSpawner _trainSpawner;

		public void Initialize(TrainSpawner trainSpawner, Rail startRail)
		{
			_health = GetComponent<Health>();
			_health.OnDieEvent.AddListener(OnDie);

			_wheelA.Initialize(startRail, transform);
			_wheelB.Initialize(startRail, transform);

			_trainSpawner = trainSpawner;

			++trainSpawner.CurrentCarCount;

			_isInitialized = true;
		}

		protected virtual void Update()
		{
			if (false == _isInitialized) return;

			if (null == _wheelA.CurrentRail)
			{
				Explosion();
			}

			if (IsHeadCar && this != headCar)
			{
				SetHeadCar(this);
			}

			SetupFinalSpeed();
		}

		public virtual void SetHeadCar(CarBase headCar)
		{
			this.headCar = headCar;
			if (null != backCar)
				backCar.SetHeadCar(headCar);
		}
	
		private void HandleTrainCompositionChange()
		{
			if(_isHandlingCompositionChange) return;
			_isHandlingCompositionChange = true;
    
			CarBase root = this;
			while (root.frontCar != null)
			{
				root = root.frontCar;
			}

			root.SpeedStack = 0;

			CarBase current = root;
			while (current != null)
			{
				current.headCar = root;
				current.OnArrayChanged();
				current = current.backCar;
			}

			_isHandlingCompositionChange = false;
		}

		public virtual void OnArrayChanged()
		{
    
		}

		public virtual void OnDie()
		{
			if (frontCar) frontCar.backCar = null;
			if (backCar) backCar.frontCar = null;
			Destroy(gameObject);
		}

		private void Explosion()
		{
			Debug.Log("Explosion");
			--_trainSpawner.CurrentCarCount;
			if (null == backCar)
			{
				DestroyWithForward();
			}
			gameObject.SetActive(false);
		}

		public void DestroyWithForward()
		{
			OnDie();

			if (null != frontCar)
				frontCar.DestroyWithForward();
		}

		private void LateUpdate()
		{
			if (false == _isInitialized) return;

			VisualMove();
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
