using UnityEngine;

namespace Core.Car.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private const float DelayBetweenShots = 0.1f;
        private const float CarSpeed = 0.05f;
        private const float Sensibility = 0.5f;

        [SerializeField] private GameObject carPrefab;

        private readonly Vector3 _startCarPosition = new Vector3(0, 0, -45);

        private CarController _carController;
        private Vector2 _lastTapPosition;
        private bool _carIsMoved;
        private float _timeAfterShot;

        public Transform CarTransform => _carController.transform;

        private void Update()
        {
            if (!_carIsMoved)
                return;

            TurretControlling();
        }

        public void Init()
        {
            _carController = Instantiate(carPrefab, transform).GetComponent<CarController>();
            InitInternal();
        }

        public void ReInit()
            => InitInternal();

        public void StartMove()
        {
            _carIsMoved = true;
            _carController.SetSpeed(CarSpeed);
        }

        public void StopMove()
        {
            _carIsMoved = false;
            _carController.SetSpeed(0);
        }

        private void InitInternal()
        {
            StopMove();
            _carController.transform.position = _startCarPosition;
        }

        private void TurretControlling()
        {
            _timeAfterShot += Time.deltaTime;

            if (Input.GetMouseButton(0))
            {
                var mousePosition = Input.mousePosition; 
                if (_lastTapPosition == Vector2.zero)
                    _lastTapPosition = mousePosition;

                var delta = _lastTapPosition.x - mousePosition.x;
                _lastTapPosition = mousePosition;
                _carController.ChangeTurretRotate(delta * Sensibility);
            }
            if (Input.GetMouseButtonUp(0))
                _lastTapPosition = Vector2.zero;

            if (_timeAfterShot >= DelayBetweenShots)
            {
                _timeAfterShot = 0;
                _carController.Shot();
            }
        }
    }
}