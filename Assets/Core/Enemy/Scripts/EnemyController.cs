using Core.Player.Scripts.Bullet;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Enemy.Scripts
{
    public class EnemyController : MonoBehaviour
    {
        private const float MaxHP = 30;
        private const float Speed = 0.02f;
        private const float InteractionDistance = 70;
        private const string AnimationTriggerName = "StartAnimation";

        [SerializeField] private Slider hpSlider;
        [SerializeField] private Animator animator;

        private GameObject _carGameObject;
        private bool _isGameStarted;
        private float _hp;

        public float DamageValue => 1.5f;

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.TryGetComponent<BulletController>(out var bulletController))
                return;

            Damage(bulletController.DamageValue);
            bulletController.gameObject.SetActive(false);
        }

        public void StartGame(GameObject carGameObject)
        {
            _hp = MaxHP;
            _isGameStarted = true;
            _carGameObject = carGameObject;
        }

        public void StopGame()
            => _isGameStarted = false;

        public void Damage(float damageValue)
        {
            if (_hp - damageValue <= 0)
            {
                DestroyEnemy();
                return;
            }

            var sliderValueLeft = damageValue / MaxHP;
            hpSlider.value -= sliderValueLeft;
            _hp -= damageValue;
        }

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

            animator.SetTrigger(AnimationTriggerName);
            transform.LookAt(_carGameObject.transform);
            transform.position += transform.forward * Speed;
        }

        private void DestroyEnemy()
        {
            hpSlider.value = 0;
            Destroy(gameObject);
        } 
    }
}
