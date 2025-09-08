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

        public void Select()
        {

        }

        public void Release()
        {

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