using UnityEngine;

namespace Core.Player.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private const float DelayBetweenShots = 0.35f;

        [SerializeField] private float carSpeed;
        [SerializeField] private float sensibility;
        [SerializeField] private GameObject carPrefab;
        [SerializeField] private GameObject destroyedCarPrefab;

        private readonly Vector3 _startCarPosition = new(0, 0, -45);

        private GameObject _destroyedCar;
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

        public void Init(Vector3 endTrackPosition)
        {
            _carController = Instantiate(carPrefab, transform).GetComponent<CarController>();
            InitInternal();
            _carController.SetEndTrackPosition(endTrackPosition);
        }

        public void ReInit()
            => InitInternal();

        public void StartGame()
        {
            _carIsMoved = true;
            _carController.StartGame();
            _carController.SetSpeed(carSpeed);
        }

        public void StopMove()
        {
            _carIsMoved = false;
            _carController.SetSpeed(0);
        }

        public void GameFailed()
        {
            _destroyedCar = Instantiate(destroyedCarPrefab, _carController.transform.position, Quaternion.identity);
            _carController.gameObject.SetActive(false);
        }

        private void InitInternal()
        {
            if (_destroyedCar != null)
                DestroyImmediate(_destroyedCar);

            StopMove();
            _carController.transform.position = _startCarPosition;
            _carController.Init();
            _carController.gameObject.SetActive(true);
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
                _carController.ChangeTurretRotate(delta * sensibility);
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