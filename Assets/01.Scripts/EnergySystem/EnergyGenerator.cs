using Project_Train.TerrainSystem;
using UnityEngine;
namespace Project_Train.EnergySystem
{

    public class EnergyGenerator : MonoBehaviour
    {
        [SerializeField] private float _detectDistane;
        [SerializeField] private LayerMask _buildPointLayer;
        [SerializeField] private Vector2 _detectSize;

        public void SupplyEnergy()
        {
            Vector3 box = new Vector3(_detectSize.x, 1f, _detectSize.y);
            RaycastHit[] hits = Physics.BoxCastAll(transform.position, box, Vector3.down, Quaternion.identity, _detectDistane, _buildPointLayer);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.TryGetComponent(out BuildPoint point))
                {
                    point.SupplyEnergy();
                }
            }
        }

        public void SubtractEnergy()
        {
            Vector3 box = new Vector3(_detectSize.x, 1f, _detectSize.y);
            RaycastHit[] hits = Physics.BoxCastAll(transform.position, box, Vector3.down, Quaternion.identity, _detectDistane, _buildPointLayer);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.TryGetComponent(out BuildPoint point))
                {
                    point.SubtractEnergy();
                }
            }
        }

    }
}