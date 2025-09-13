using UnityEngine;
namespace Project_Train.ObjectManage.VFX
{

    public class ParticleObject : VFXObject
    {
        [SerializeField] private ParticleSystem _particle;

        protected override void PlayEffect()
        {
            _particle.Play();
            
        }
    }
}