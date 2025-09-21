using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
namespace CameraControllers
{

    public class CameraOffsetController : MonoBehaviour, ICameraControlable
    {
        private CinemachineCamera _camera;
        private CinemachinePositionComposer _positionComposer;

        public void Initialize(CinemachineCamera camera)
        {
            _camera = camera;
            _positionComposer = _camera.GetComponent<CinemachinePositionComposer>();
        }

        public void MovePositionOffset(Vector2 offset, float duration)
        {
            StartCoroutine(MovePositionOffsetCoroutine(offset, duration));

        }

        private IEnumerator MovePositionOffsetCoroutine(Vector2 offset, float duration)
        {
            float currentTime = 0f;
            Vector3 previousOffset = _positionComposer.TargetOffset;
            while (currentTime < duration)
            {
                currentTime += Time.time;
                float ratio = currentTime / duration;
                SetPositionOffset(Vector2.Lerp(previousOffset, offset, ratio));
                yield return null;
            }
        }

        public void SetPositionOffset(Vector2 offset)
        {
            _camera.transform.position = new Vector3(offset.x, offset.y, _camera.transform.position.z);
        }
    }
}