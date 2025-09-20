using Project_Train.RailSystem;
using Project_Train.Utils;
using System;
using System.Collections;
using UnityEngine;

namespace  Project_Train.Combat.TrainSystem
{
	public class TrainSpawner : ICoroutineableObject<MonoBehaviour>
    {
		public Rail startRail;

		public MonoBehaviour Owner { get; set;  }

		private CarBase _preiousSpawnedCarBase = null;

        public event Action OnTrainArraySpawnComplete;

		public void Initialize(MonoBehaviour owner)
		{
			Owner = owner;
		}

		public void Spawn(TrainArraySO trainArraySO)
        {
			Owner.StartCoroutine(CoroutineSpawn(trainArraySO));
        }

        private IEnumerator CoroutineSpawn(TrainArraySO trainArraySO)
        {
            for (int i = 0; i < trainArraySO.Count; i++)
            {
                CarBase newCar = GameObject.Instantiate(trainArraySO[i], startRail.transform.position, startRail.transform.rotation);

				if (null != _preiousSpawnedCarBase)
				{
					newCar.frontCar = _preiousSpawnedCarBase;
					newCar.headCar = _preiousSpawnedCarBase.headCar;
					_preiousSpawnedCarBase.backCar = newCar;
				}

                _preiousSpawnedCarBase = newCar;

                newCar.Initialize(startRail);

				// When completely off the start rail
				yield return new WaitUntil(() => newCar.CurrentRail != startRail);
				yield return new WaitForSeconds(0.25f);
			}

			OnTrainArraySpawnComplete?.Invoke();
		}
	}
}
