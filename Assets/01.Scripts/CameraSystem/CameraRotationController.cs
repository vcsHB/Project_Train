using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
namespace CameraControllers
{

    public class CameraRotationController : MonoBehaviour, ICameraControlable
    {
        private CinemachineCamera _camera;
        [SerializeField] private float _defaultRotation;

        public void Initialize(CinemachineCamera camera)
        {
            _camera = camera;
        }

        public void Rotate(float angle, float duration, bool useUnscaledTime = false)
        {
            _camera.transform.DOLocalRotate(new Vector3(0f, 0f, angle), duration).SetUpdate(useUnscaledTime);
        }

        public void ResetRotation()
        {
            _camera.transform.eulerAngles = new Vector3(
                0f, 0f, _defaultRotation);
        }
    }
}