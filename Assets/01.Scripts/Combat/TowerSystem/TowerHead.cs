using UnityEngine;
namespace Project_Train.Combat.TowerSystem
{
    public class TowerHead : MonoBehaviour
    {
        [SerializeField] private float _aimingSpeed = 5f;
        [SerializeField] private float _aimAllowRange = 2f;

        /// <summary>
        /// TowerHead
        /// </summary>
        /// <param name="direction">목표 위치 - 현재 위치 (월드 기준 방향 벡터)</param>
        /// <returns>총구가 목표 각도 범위 안에 있으면 true</returns>
        public bool SetAimRotation(Vector3 direction)
        {
            direction.y = 0f;
            if (direction.sqrMagnitude < 0.001f)
                return false; // 목표가 없는 경우

            Quaternion targetRot = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRot,
                _aimingSpeed * Time.deltaTime
            );

            float angle = Quaternion.Angle(transform.rotation, targetRot);

            return angle <= _aimAllowRange;
        }
    }
}
