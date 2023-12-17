using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Core.Code.Camera
{
    public class CameraController : MonoBehaviour
    {
        private readonly Vector3 _startPosition = new Vector3(-7.97f, 11.98f, -54.89f);
        private readonly Vector3 _endPosition = new Vector3(0, 18.8f, -57.5f);
        private readonly Vector3 _startRotation = new Vector3(56.3f, 40.34f, 5.7f);
        private readonly Vector3 _endRotation = new Vector3(44.6f, 0, 0);

        private bool _isGameStarted;
        private Vector3 _startOffset;
        private Transform _targetTransform;

        public void Init(Transform targetTransform)
        {
            _targetTransform = targetTransform;
            _isGameStarted = false;
            transform.position = _startPosition;
            transform.DORotate(_startRotation, 0);
        }

        public async UniTask StartGameCameraPositionAnimation()
        {
            transform.DOMove(_endPosition, 0.5f);
            await transform.DORotate(_endRotation, 0.5f).AsyncWaitForCompletion();

            _startOffset = _targetTransform.position - transform.position;

            _isGameStarted = true;
        }

        private void Update()
        {
            if (!_isGameStarted)
                return;

            transform.position =
                new Vector3(_targetTransform.position.x - _startOffset.x, transform.position.y, _targetTransform.position.z - _startOffset.z);
        }
    }
}
