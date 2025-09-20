using Project_Train.RailSystem;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Project_Train.Combat.TrainSystem
{
	public class TrainSpawner
	{
		public Rail startRail;

		private CarBase _preiousSpawnedCarBase = null;

		public event Action OnTrainArraySpawnComplete;
		public event Action<int> OnLeftCarAmountChangeEvent;

		private int _currentCarCount;
		public int CurrentCarCount
		{
			get { return _currentCarCount; }
			set
			{
				OnLeftCarAmountChangeEvent?.Invoke(_currentCarCount);
				_currentCarCount = value;
			}
		}

		public IEnumerator CoroutineSpawn(TrainArraySO trainArraySO)
		{
			for (int i = 0; i < trainArraySO.Count; i++)
			{
				CarBase newCar = GameObject.Instantiate(trainArraySO[i], startRail.transform.position, startRail.transform.rotation);

				if (_preiousSpawnedCarBase)
				{
					newCar.frontCar = _preiousSpawnedCarBase;
					newCar.headCar = _preiousSpawnedCarBase.headCar;
					_preiousSpawnedCarBase.backCar = newCar;
				}

				_preiousSpawnedCarBase = newCar;

				newCar.Initialize(this, startRail);

				// When completely off the start rail
				yield return new WaitUntil(() => newCar.CurrentRail != startRail);
				yield return new WaitForSeconds(0.25f);
			}
			_preiousSpawnedCarBase = null;
			OnTrainArraySpawnComplete?.Invoke();
		}
	}
}
