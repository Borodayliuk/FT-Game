using Core.Bullet;
using Core.Car.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Enemy.Scripts
{
    public class EnemyController : MonoBehaviour
    {
        private const float MaxHP = 30;
        private const float Speed = 0.02f;
        private const float InteractionDistance = 70;

        [SerializeField] private Slider hpSlider;

        private GameObject _carGameObject;
        private bool _isGameStarted;
        private float _hp;

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.TryGetComponent<BulletController>(out var bulletController))
                return;

            Damage(bulletController.DamageValue);
            bulletController.gameObject.SetActive(false);
            bulletController.SetStartParameters();
        }

        public void StartGame()
        {
            _hp = MaxHP;
            _isGameStarted = true;
            _carGameObject = FindObjectOfType<CarController>().gameObject;
        }

        public void StopGame()
            => _isGameStarted = false;

        private void Update()
        {
            if (!_isGameStarted)
                return;

            EnemyControlling();
        }

        private void EnemyControlling()
        {
            var distance = Vector3.Distance(transform.position, _carGameObject.transform.position);

            if (distance > InteractionDistance)
                return;

            transform.LookAt(_carGameObject.transform);
            transform.position += transform.forward * Speed;
        }

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
            Destroy(gameObject);
        } 
    }
}
