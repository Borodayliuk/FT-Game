using UnityEngine;
using UnityEngine.UI;

namespace Core.Car.Scripts
{
    public class CarController : MonoBehaviour
    {
        private const float MaxHP = 100;

        [SerializeField] private TurretController turret;
        [SerializeField] private Slider hpSlider;

        private Vector3 _speed = Vector3.zero;
        private float _hp;

        private void Update()
        {
            transform.position += _speed;

            if (Input.GetKeyUp(KeyCode.H))
            {
                Damage(11);
            }
        }

        public void Init()
        {
            _hp = MaxHP;
            hpSlider.gameObject.SetActive(false);
            hpSlider.value = 1;
        }

        public void StartGame()
        {
            hpSlider.gameObject.SetActive(true);
        }

        public void Shot()
            => turret.Shot();

        public void SetSpeed(float speed)
            => _speed = Vector3.forward * speed;

        public void ChangeTurretRotate(float rotate)
            => turret.ChangeRotate(rotate);

        private void Damage(float damageValue)
        {
            if (_hp - damageValue <= 0)
            {
                GameFailed();
                return;
            }

            var p = damageValue / MaxHP;
            hpSlider.value -= p;
            _hp -= damageValue;
        }

        private void GameFailed()
        {
            hpSlider.value = 0;
            GlobalGameEvents.GameLoss?.Invoke();
        }
    }
}
