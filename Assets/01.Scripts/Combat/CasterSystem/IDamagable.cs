using Core.Attribute;
using UnityEngine;

namespace Project_Train.Combat.CasterSystem
{
    [System.Serializable]
    public struct DamageData
    {
        public float damage;
        [ReadOnly] public Vector3 originPosition;
        [ReadOnly] public Vector3 damageDirection;
        public bool ignoreResist;
        public bool ignoreDamageCooltime;
        // Transfrom originTrm ??


    }

    public struct HitResponse
    {
        public bool isHit;
        public float reflectDamage;

    }
    public interface IDamageable
    {
        public HitResponse ApplyDamage(DamageData damageData);
    }
}