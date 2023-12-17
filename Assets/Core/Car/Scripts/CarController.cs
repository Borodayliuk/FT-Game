using UnityEngine;

namespace Core.Car.Scripts
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private TurretController turret;

        private Vector3 _speed = Vector3.zero;

        private void Update()
        {
            transform.position += _speed;
        }

        public void Shot()
            => turret.Shot();
        public void SetSpeed(float speed)
            => _speed = Vector3.forward * speed;

        public void ChangeTurretRotate(float rotate)
            => turret.ChangeRotate(rotate);
    }
}
