using UnityEngine;
namespace Project_Train.Combat.TowerSystem
{

    public abstract class GunBarrelPart : TowerPart
    {
        [Space(5f)]
        [Header("GunBarrel Settings")]
        protected TargetData _targetData;
        public virtual void Initialize(TargetData targetData)
        {
            _targetData = targetData;

        }
        public abstract void Shoot();

        public virtual void SetVerticalAngle(float angle)
        {
            transform.localRotation = Quaternion.Euler(-angle, 0f, 0f);
        }


    }
}