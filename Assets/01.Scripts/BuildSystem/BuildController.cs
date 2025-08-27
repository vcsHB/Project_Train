using System;
using System.Collections.Generic;
using Project_Train.Core.Input;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
namespace Project_Train.BuildSystem
{

    public class BuildController : MonoBehaviour
    {
        [SerializeField] private BuildSelector _buildSelector;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _buildPointLayer;
        [SerializeField] private LayerMask _structureLayer;
        private readonly string _mouseEventKey = "OnMousePositionEvent";
        private readonly string _selectEventKey = "OnSelectEvent";
        private float _detectMaxDistance = 100f;
        private Vector2 _mousePosition;


        private void Awake()
        {
            InputReader.AddListener<Vector2>(_mouseEventKey, HandleMousePositionSet);
            InputReader.AddListener(_selectEventKey, HandleSelect);
        }

        private void HandleMousePositionSet(Vector2 mousePos)
        {
            _mousePosition = mousePos;
        }

        private void HandleSelect()
        {
            Ray ray = Camera.main.ScreenPointToRay(_mousePosition);
            bool isDetected = Physics.Raycast(ray, out RaycastHit hitInfo, _detectMaxDistance, _buildPointLayer);
            if (isDetected)
            {

            }

            _buildSelector.SetPosition(hitInfo.point);
        }
    }
}