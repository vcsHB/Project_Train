using UnityEngine;

namespace Project_Train.Combat.TowerSystem.SubVisuals
{

    public class RocketBarrelUnit : SubVisualObject
    {
        [SerializeField] private ParticleSystem _fireParticle;
        [SerializeField] private Transform _firePositionTrm;
        public Transform FirePos => _firePositionTrm;

        private void Awake()
        {
            _firePositionTrm = transform.Find("FirePos");
        }

        public override void PlayVisualEffect()
        {
            _fireParticle.transform.position = _firePositionTrm.position;
            _fireParticle.Play();

        }
    }
}