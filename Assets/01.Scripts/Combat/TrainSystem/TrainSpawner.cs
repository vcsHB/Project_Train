using Project_Train.RailSystem;
using System;
using System.Collections;
using UnityEngine;

namespace  Project_Train.Combat.TrainSystem
{
    public class TrainSpawner : MonoBehaviour
    {
        [field: SerializeField] public Rail StartRail { get; private set; }

        private CarBase _preiousSpawnedCarBase;

        public event Action OnTrainArraySpawnComplete;

        public void Spawn(TrainArraySO trainArraySO)
        {
            StartCoroutine(CoroutineSpawn(trainArraySO));
        }

        private IEnumerator CoroutineSpawn(TrainArraySO trainArraySO)
        {
            for (int i = 0; i < trainArraySO.Count; i++)
            {
                CarBase newCar = Instantiate(trainArraySO[i], StartRail.transform.position, StartRail.transform.rotation);
				newCar.startRail = StartRail;

				if (null != _preiousSpawnedCarBase)
				{
					newCar.frontCar = _preiousSpawnedCarBase;
                    _preiousSpawnedCarBase.backCar = newCar;
				}

                _preiousSpawnedCarBase = newCar;

                newCar.Initialize();

				// When completely off the start rail
				yield return new WaitUntil(() => newCar.startRail != StartRail);
			}
		}
	}
}
