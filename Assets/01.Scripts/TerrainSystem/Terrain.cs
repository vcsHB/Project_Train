using System;
using System.Collections.Generic;
using Project_Train.TerrainSystem;
using UnityEngine;
namespace Project_Train.LevelSystem
{

    public class Terrain : MonoBehaviour
    {
        [Header("Terrain Essential Setting")]
        [SerializeField] private Vector2Int _gridTiling = new Vector2Int(20, 20);
        [SerializeField] private Vector2 _gridTilingOffset;
        [SerializeField] private BuildPoint _buildPointPrefab;
        [SerializeField] private float _tilingTerm = 10f;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _ignoreLayer;
        [SerializeField] private float _detectHeight = 5f;
        [SerializeField] private float _groundDetectDistance = 10f;
        [SerializeField] private Transform _buildPointGroupParentTrm;
        private Vector3 _bakeStartEdge;
        private Vector3 _bakeEndEdge;
        [SerializeField] private List<BuildPoint> _buildPointList = new();

        [ContextMenu("BakeTerrain")]
        public void BakeTerrain()
        {
            ClearBuildPoint();
            float centerOffset = _tilingTerm * 0.5f;
            for (int i = 0; i < _gridTiling.x; i++)
            {
                for (int j = 0; j < _gridTiling.y; j++)
                {
                    Vector3 detectPosition = _bakeEndEdge + new Vector3(i * _tilingTerm + centerOffset, 0f, j * _tilingTerm + centerOffset);

                    bool isIgnore = Physics.Raycast(detectPosition, Vector2.down, _groundDetectDistance, _ignoreLayer);
                    if (isIgnore) continue;

                    bool isDetected = Physics.Raycast(detectPosition, Vector2.down, out RaycastHit hitInfo, _groundDetectDistance, _groundLayer);
                    if (isDetected)
                    {
                        GenerateBuildPoint(hitInfo.point, i, j);
                    }
                }
            }

        }
        [ContextMenu("Clear")]
        private void ClearBuildPoint()
        {
            for (int i = 0; i < _buildPointList.Count; i++)
            {
                _buildPointList[i].DestroyPoint();
            }
            _buildPointList.Clear();
        }

        private void GenerateBuildPoint(Vector3 position, int xIndex, int zIndex)
        {
            BuildPoint newPoint = Instantiate(_buildPointPrefab, _buildPointGroupParentTrm);
            newPoint.gameObject.name = $"BuildPoint ({xIndex}-{zIndex})";
            newPoint.transform.position = position;
            _buildPointList.Add(newPoint);

        }


#if UNITY_EDITOR

        private void OnValidate()
        {
            float xDelta = _gridTiling.x * 0.5f * _tilingTerm;
            float zDelta = _gridTiling.y * 0.5f * _tilingTerm;
            Vector3 offset = new Vector3(_gridTilingOffset.x, 0f, _gridTilingOffset.y);
            _bakeStartEdge = transform.position + new Vector3(xDelta, _detectHeight, zDelta) + offset;
            _bakeEndEdge = transform.position + new Vector3(-xDelta, _detectHeight, -zDelta) + offset;

        }

        private void OnDrawGizmosSelected()
        {
            if (_groundDetectDistance < 0f) return;
            Gizmos.color = new Color(1f, 0f, 0f, 0.1f);
            Gizmos.DrawCube(
                transform.position +
                new Vector3(
                    0f,
                    -_groundDetectDistance * 0.5f + _detectHeight,
                    0f),
                new Vector3(
                    _tilingTerm * _gridTiling.x,
                    _groundDetectDistance,
                    _tilingTerm * _gridTiling.y)
            );
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(
                            transform.position +
                            new Vector3(
                                0f,
                                -_groundDetectDistance * 0.5f + _detectHeight,
                                0f),
                            new Vector3(
                                _tilingTerm * _gridTiling.x,
                                _groundDetectDistance,
                                _tilingTerm * _gridTiling.y)
                        );
        }

#endif




    }
}