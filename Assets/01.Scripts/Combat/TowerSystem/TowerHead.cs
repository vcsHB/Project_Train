using UnityEngine;
namespace Project_Train.Combat.TowerSystem
{
    public class TowerHead : MonoBehaviour
    {
        [SerializeField] private GunBarrelPart _gunBarrelPart;
        [SerializeField] private float _headAimingSpeed = 5f;
        [SerializeField] private float _verticalAimingSpeed = 30f;
        [SerializeField] private float _aimAllowRange = 2f;
        private TargetData _targetData;


        /// <summary>
        /// TowerHead Rotation To Target 
        /// </summary>
        /// <param name="targetPosition">Direction to Target (TargetLocation - CurrentLocation)</param>
        /// <returns>true : Aim Aligned</returns>
        public bool SetAimRotation(Vector3 targetPosition)
        {
            Vector3 gunBarrelPosition = _gunBarrelPart.transform.position;
            Vector3 direction = (targetPosition - gunBarrelPosition).normalized;
            Vector3 horizontalDirection = direction;
            horizontalDirection.y = 0f;
            Quaternion headTargetRotation = Quaternion.LookRotation(horizontalDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, headTargetRotation, _headAimingSpeed * Time.deltaTime);

            float verticalAngle = Mathf.Atan2(direction.y, horizontalDirection.magnitude) * Mathf.Rad2Deg;
            _gunBarrelPart.SetVerticalAngle(verticalAngle);

            return Quaternion.Angle(transform.rotation, headTargetRotation) < _aimAllowRange;
        }

        public void Initialize(TargetData targetData)
        {
            _targetData = targetData;
            _gunBarrelPart.Initialize(targetData);
        }

        public void Shoot()
        {
            _gunBarrelPart.Shoot();
        }
    }
}
