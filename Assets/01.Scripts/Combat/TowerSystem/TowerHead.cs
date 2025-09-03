using System;
using UnityEngine;
namespace Project_Train.Combat.TowerSystem
{
    public class TowerHead : MonoBehaviour
    {
        [SerializeField] private GunBarrelPart _gunBarrelPart;
        [SerializeField] private float _aimingSpeed = 5f;
        [SerializeField] private float _aimAllowRange = 2f;
        private TargetData _targetData;

        /// <summary>
        /// TowerHead Rotation To Target 
        /// </summary>
        /// <param name="targetPosiotion">Direction to Target (TargetLocation - CurrentLocation)</param>
        /// <returns>true : Aim Aligned</returns>
        public bool SetAimRotation(Vector3 targetPosition)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _aimingSpeed * Time.deltaTime);

            return Quaternion.Angle(transform.rotation, targetRotation) < _aimAllowRange;
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
