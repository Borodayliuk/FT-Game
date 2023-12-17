using System.Collections.Generic;
using Core.Enemy.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Player.Scripts
{
    public class CarController : MonoBehaviour
    {
        private const float MaxHp = 350;

        [SerializeField] private TurretController turret;
        [SerializeField] private Slider hpSlider;
        [SerializeField] private GameObject smokeEffect;
        [SerializeField] private List<TrailRenderer> trailRenderers;
        [SerializeField] private GameObject laser;

        private Vector3 _speed = Vector3.zero;
        private Vector3 _endTrackPosition;
        private float _hp;
        private bool _isGameStarted;

        private void OnEnable()
        {
            ClearTrails();
            hpSlider.interactable = false;
        }

        private void Update()
        {
            if (!_isGameStarted)
                return;

            CarControlling();
        }

        private void OnCollisionStay(Collision collision)
        {
            if (!_isGameStarted)
                return;

            if (collision.gameObject.TryGetComponent<EnemyController>(out var enemyController))
                Damage(enemyController.DamageValue);
        }

        public void Init()
        {
            _isGameStarted = false;
            _hp = MaxHp;

            laser.SetActive(false);
            ClearTrails();
            hpSlider.gameObject.SetActive(false);
            hpSlider.value = 1;
            smokeEffect.SetActive(false);
        }

        public void SetEndTrackPosition(Vector3 endTrackPosition)
            => _endTrackPosition = endTrackPosition;

        public void StartGame()
        {
            _isGameStarted = true;
            hpSlider.gameObject.SetActive(true);
            smokeEffect.SetActive(true);
            laser.SetActive(true);
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

            var p = damageValue / MaxHp;
            hpSlider.value -= p;
            _hp -= damageValue;
        }

        private void GameFailed()
        {
            _isGameStarted = false;
            hpSlider.value = 0;
            GlobalGameEvents.GameFailed?.Invoke();
        }

        private void CarControlling()
        {
            transform.position += _speed;

            if (transform.position.z >= _endTrackPosition.z)
                GlobalGameEvents.LevelCompleted?.Invoke();
        }

        private void ClearTrails()
        {
            foreach (var trailRenderer in trailRenderers)
                trailRenderer.Clear();
        }
    }
}
