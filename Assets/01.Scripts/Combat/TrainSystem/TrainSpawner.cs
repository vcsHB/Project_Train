using Project_Train.RailSystem;
using System.Collections;
using UnityEngine;

namespace  Project_Train.Combat.TrainSystem
{
    public class TrainSpawner : MonoBehaviour
    {
        [field: SerializeField] public Rail StartRail { get; private set; }

        public void Spawn(TrainArraySO trainArraySO)
        {
            StartCoroutine(CoroutineSpawn(trainArraySO));
        }

        private IEnumerator CoroutineSpawn(TrainArraySO trainArraySO)
        {
            for (int i = 0; i < trainArraySO.Count; i++)
            {
                var car = Instantiate(trainArraySO[i], StartRail.transform.position, StartRail.transform.rotation);
				car.currentRail = StartRail;

				// When completely off the start rail
				yield return new WaitUntil(() => car.currentRail != StartRail);
			}
		}
	}
}
