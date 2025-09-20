using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Project_Train.BuildSystem
{
    public class BuildSelector : MonoBehaviour
    {
        [SerializeField] private DecalProjector _rangeDecalProjector;
        [SerializeField] private float _drawDistane = 100f;

        private Material _decalMaterial;
        private readonly int _ignoreRadiusRatioHash = Shader.PropertyToID("_IgnoreRadiusRatio");
        private void Awake()
        {
            _decalMaterial = _rangeDecalProjector.material;

        }
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetTowerRangeVisual(float radius, float ignoreRatio = 0f)
        {
            if (Mathf.Approximately(radius, 0f))
            {
                _rangeDecalProjector.enabled = false;

            }
            else
            {
                _rangeDecalProjector.enabled = true;
                _decalMaterial.SetFloat(_ignoreRadiusRatioHash, ignoreRatio);
                _rangeDecalProjector.size = new Vector3(radius * 2f, radius * 2f, _drawDistane);
            }

        }
    }

}
