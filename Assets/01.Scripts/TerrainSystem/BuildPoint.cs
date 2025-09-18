using System;
using Project_Train.BuildSystem;
using UnityEngine;
namespace Project_Train.TerrainSystem
{

    public class BuildPoint : MonoBehaviour
    {

        // Properties
        public Transform PointTrm => transform;
        public Vector3 PointPosition => transform.position;
        [SerializeField] private TerrainStatus _terrainStatus;
        public TerrainStatus TerrainStatus => _terrainStatus;
        [SerializeField] private Building _buildingOnPoint;
        [field: SerializeField] public bool CanBuild { get; private set; } = true;
        public void Select()
        {

        }

        public void Release()
        {

        }

        public void SetCanBuild(bool value)
        {
            CanBuild = value;
        }

        public void SetBuild(Building building)
        {
            _buildingOnPoint = building;
            _buildingOnPoint.OnBuildingDestroyEvent.AddListener(HandleBuildingDestroy);
        }

        private void HandleBuildingDestroy()
        {
            _buildingOnPoint = null;
            SetCanBuild(true);
        }

        public void SetTerrainData(TerrainStatus status)
        {
            _terrainStatus = status;
        }

        public void DestroyPoint()
        {
#if UNITY_EDITOR
            DestroyImmediate(gameObject);
#else
            Destroy(gameObject);
#endif
        }
    }
}