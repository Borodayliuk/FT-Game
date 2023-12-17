using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Car.Scripts
{
    public class BulletsPool : MonoBehaviour
    {
        private const int CountPooledBullets = 50;

        [SerializeField] private GameObject bulletPrefab;

        private readonly List<GameObject> _bullets = new();

        public void InitBullets()
        {
            for (var i = 0; i < CountPooledBullets; i++)
            {
                var bullet = Instantiate(bulletPrefab);
                bullet.SetActive(false);
                _bullets.Add(bullet);
            }
        }

        public GameObject GetBullet()
        {
            foreach (var bullet in _bullets)
            {
                if (!bullet.activeInHierarchy)
                {
                    return bullet;
                }
            }

            var newBullet = Instantiate(bulletPrefab);
            newBullet.SetActive(false);
            _bullets.Add(newBullet);

            return newBullet;
        }
    }
}