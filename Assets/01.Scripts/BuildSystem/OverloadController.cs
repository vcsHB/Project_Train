using UnityEngine;
namespace Project_Train.BuildSystem

{
    public class OverloadController : MonoBehaviour, IOverloadable
    {
        [Header("Overload Informations")]
        [SerializeField] protected float _overloadLevel = 1f;
        [SerializeField] protected float _overloadDuration = 5f;
        protected readonly float _defualtOverloadLevel = 1f;
        private bool _overloaded;
        private float _overloadExpireTime;

        public float CurreentOverloadLevel
        {
            get
            {
                if (_overloaded)
                {
                    if (Time.time > _overloadExpireTime)
                    {
                        _overloadLevel = _defualtOverloadLevel;
                        _overloaded = false;
                    }
                }
                return _overloadLevel;
            }
        }


        public void ApplyOverload(float amount)
        {
            _overloadLevel = amount;
            _overloaded = true;
            _overloadExpireTime = Time.time + _overloadDuration;
        }
    }
}