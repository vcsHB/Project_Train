using System;
using System.Collections.Generic;
using Project_Train.BuildSystem.SubObjects;
using Project_Train.DataManage.CoreDataBaseSystem;
using Project_Train.ResourceSystem;
using Project_Train.TerrainSystem;
using UnityEngine;
namespace Project_Train.BuildSystem
{

    public class BuildingManager : MonoBehaviour
    {
        [SerializeField] private ResourceManagerSO _resourceManager;
        [SerializeField] private List<Building> _buildingList;
        [Header("BuildFrame Essential Settings")]

        [SerializeField] private BuildFrame _buildFramePrefab;
        [SerializeField] private int _initializePoolAmount = 10;

        private Stack<BuildFrame> _buildFramePool;



        private void Awake()
        {
            InitializePool();
        }

        private void InitializePool()
        {
            _buildFramePool = new();
            for (int i = 0; i < _initializePoolAmount; i++)
            {
                BuildFrame buildFrame = GenerateNewBuildFrame();
                buildFrame.gameObject.SetActive(false);
                _buildFramePool.Push(buildFrame);
            }

        }

        private BuildFrame GetNewBuildFrame()
        {
            BuildFrame newFrame = _buildFramePool.Count > 0 ?
                _buildFramePool.Pop() : GenerateNewBuildFrame();
            newFrame.gameObject.SetActive(true);

            return newFrame;

        }

        private void HandleReturnFrame(BuildFrame frame)
        {
            frame.gameObject.SetActive(false);
            _buildFramePool.Push(frame);

        }

        private BuildFrame GenerateNewBuildFrame()
        {
            BuildFrame newFrame = Instantiate(_buildFramePrefab);
            newFrame.OnFrameReturnEvent += HandleReturnFrame;
            return newFrame;
        }

        public void Build(BuildingDataSO buildingData, BuildDetailSO detail, BuildPoint point)
        {
            if (!point.CanBuild) return;
            foreach (var require in detail.RequireResource)
            {
                if (!_resourceManager.IsEnough(require.Key.resourceType, require.Value))
                {

                    return;
                }
            }
            foreach (var require in detail.RequireResource)
            {
                _resourceManager.TrySubtractResource(require.Key.resourceType, require.Value);
            }

            point.SetCanBuild(false);
            BuildFrame buildFrame = GetNewBuildFrame();
            buildFrame.StartBuild(buildingData, detail, point);

        }
    }
}