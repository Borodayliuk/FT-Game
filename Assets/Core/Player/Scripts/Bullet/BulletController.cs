using UnityEngine;

namespace Core.Player.Scripts.Bullet
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private TrailRenderer trailRenderer;

        private Vector3 _moveVector;

        public float DamageValue => 10;

        private void OnEnable()
        {
            _moveVector = -transform.up * speed;
            trailRenderer.Clear();
        }

        private void Update()
        {
            transform.position += _moveVector;
        }
    }
}