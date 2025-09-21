using Project_Train.Combat.CasterSystem.HitBody;
using Project_Train.RailSystem;
using UnityEngine;

namespace Project_Train.Combat.TrainSystem
{
	public abstract class CarBase : MonoBehaviour
	{
		public CarBase headCar;
		public CarBase frontCar;
		public CarBase backCar;
		private bool _isHandlingCompositionChange;

		[Header("TrainArrays")]
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

		private Collider _selfCollider;
		private Collider[] _trainColliders = new Collider[2];
		[SerializeField] private LayerMask layerMask;

		public void Initialize(TrainSpawner trainSpawner, Rail startRail)
		{
			_selfCollider = GetComponent<Collider>();
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

			CheckFrontCar();

			if (null == _wheelA.CurrentRail)
			{
				Explosion();
			}

			if (IsHeadCar && this != headCar)
			{
				SpeedStack = 0f;
				SetHeadCar(this);
			}

			SetupFinalSpeed();
		}

		private void CheckFrontCar()
		{
			if (false == IsHeadCar) return;

			var pos = transform.position + transform.forward.normalized * RailMath.RailLength;
			if (Physics.OverlapSphereNonAlloc(pos, 0.5f, _trainColliders, layerMask) > 0)
			{
				for (int i = 0; i < _trainColliders.Length; i++)
				{
					if (_selfCollider == _trainColliders[i]) continue;
					if (_trainColliders[i] == null) continue;
					if (_trainColliders[i].TryGetComponent(out CarBase newCarBase))
					{
						frontCar = newCarBase;
						newCarBase.backCar = this;
						SetHeadCar(newCarBase.headCar);
						return;
					}
				}
			}
		}

		public virtual void SetHeadCar(CarBase headCar)
		{
			this.headCar = headCar;
			if (null != backCar)
				backCar.SetHeadCar(headCar);
		}
	
		private void OnDestroy()
		{
			if (headCar?.headCar)
			{
				headCar.headCar = null;
			}
		}

		public virtual void OnDie()
		{
			if (frontCar) frontCar.backCar = null;
			if (backCar) backCar.frontCar = null;
			Destroy(gameObject);
		}

		private void Explosion()
		{
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
