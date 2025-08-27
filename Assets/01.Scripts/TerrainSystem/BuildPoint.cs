using UnityEngine;
namespace Project_Train.TerrainSystem
{

    public class BuildPoint : MonoBehaviour
    {

        // Properties
        public Transform PointTrm => transform;
        public Vector3 PointPosition => transform.position;

        public void Select()
        {

        }

        public void Release()
        {

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