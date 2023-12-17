using UnityEngine;

namespace Core.Bullet
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speed;

        private Vector3 _moveVector;

        private void OnEnable()
        {
            _moveVector = -transform.up * speed;
        }

        private void Update()
        {
            transform.position += _moveVector;
        }

        public void SetStartParameters()
        { 
            _moveVector = Vector3.zero;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }
    }
}