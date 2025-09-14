using System;
using Crogen.CrogenPooling;
using Project_Train.Combat.CasterSystem.HitBody;
using Project_Train.DataManage.CoreDataBaseSystem;
using UnityEngine;
using UnityEngine.Events;
namespace Project_Train.BuildSystem.SubObjects
{

    public class BuildFrame : MonoBehaviour
    {
        public event Action<float, float> OnBuildProgressChangeEvent;
        public event Action<Building> OnBuildCompleteEvent;
        public UnityEvent OnBuildCompleteUnityEvent;
        [SerializeField] private float _buildDuration;
        private Health HealthCompo;

        private float _currentTime = 0f;
        private bool _isBuildStarted;
        private BuildingDataSO _buildData;
        private BuildDetailSO _detailData;
        private Vector3 _buildPosition;

        public event Action<BuildFrame> OnFrameReturnEvent;

        private void Awake()
        {
            HealthCompo = GetComponent<Health>();
            if (HealthCompo == null)
            {
                Debug.LogError("Can't Find Health Component in BuildFrame");
            }
        }

        public void StartBuild(BuildingDataSO buildingData, BuildDetailSO buildDetail, Vector3 position)
        {
            _detailData = buildDetail;
            _buildDuration = buildDetail.buildDuration;
            _buildData = buildingData;
            _detailData = buildDetail;
            _buildPosition = position;
            _isBuildStarted = true;

            HealthCompo.FillHealthToMax();

        }

        private void Update()
        {
            if (_isBuildStarted)
            {
                _currentTime += Time.deltaTime;
                OnBuildProgressChangeEvent?.Invoke(_currentTime, _buildDuration);
                if (_currentTime > _buildDuration)
                {
                    HandleBuildComplete();
                }
            }
        }

        private void HandleBuildComplete()
        {

            Building building = Instantiate(_buildData.buildingPrefab, _buildPosition, Quaternion.identity);
            _isBuildStarted = false;

            OnBuildCompleteEvent?.Invoke(building);
            OnBuildCompleteUnityEvent?.Invoke();
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            OnFrameReturnEvent?.Invoke(this);

        }

    }
}