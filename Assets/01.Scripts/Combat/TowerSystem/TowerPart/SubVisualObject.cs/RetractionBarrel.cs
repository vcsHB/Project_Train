using System.Collections;
using DG.Tweening;
using UnityEngine;
namespace Project_Train.Combat.TowerSystem.SubVisuals
{
    public class RetractionBarrel : SubVisualObject
    {
        [Header("Essential Settings")]
        [SerializeField] private Transform _barrelTrm;
        [SerializeField] private Ease _retractionEase;
        [SerializeField] private Vector3 _retractionScale;
        [SerializeField] private float _retractionDuration = 0.1f;
        [SerializeField] private float _returnDuration = 0.2f;
        [SerializeField] private Ease _returnEase;
        private Vector3 _defaultPosition;
        private Vector3 _retractionPosition;

        private void Awake()
        {
            _defaultPosition = _barrelTrm.localPosition;
            _retractionPosition = _defaultPosition + _retractionScale;
        }

        public override void PlayVisualEffect()
        {
            _barrelTrm.DOLocalMove(_retractionPosition, _retractionDuration).SetEase(_retractionEase).OnComplete(() =>
            {
                _barrelTrm.DOLocalMove(_defaultPosition, _returnDuration).SetEase(_returnEase);
            });
        }

    }
}