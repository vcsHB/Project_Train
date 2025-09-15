using System;
using System.Collections.Generic;
using Project_Train.BuildSystem.SubObjects;
using Project_Train.DataManage.CoreDataBaseSystem;
using UnityEngine;
namespace Project_Train.BuildSystem
{

    public class BuildingManager : MonoBehaviour
    {
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
                _buildFramePool.Push(buildFrame);
            }

        }

        private BuildFrame GetNewBuildFrame()
        {
            BuildFrame newFrame = _buildFramePool.Count > 0 ?
                _buildFramePool.Pop() : GenerateNewBuildFrame();


            return newFrame;

        }

        private void HandleReturnFrame(BuildFrame frame)
        {
            _buildFramePool.Push(frame);            
            
        }

        private BuildFrame GenerateNewBuildFrame()
        {
            BuildFrame newFrame = Instantiate(_buildFramePrefab);
            newFrame.OnFrameReturnEvent += HandleReturnFrame;
            return newFrame;
        }



        public void Build(BuildingDataSO buildingData, Vector3 position)
        {
            BuildDetailSO detail = buildingData.GetDetail<BuildDetailSO>(DataDetailType.Build);
            BuildFrame buildFrame = GetNewBuildFrame();
            buildFrame.StartBuild(buildingData, detail, position);

        }
    }
}