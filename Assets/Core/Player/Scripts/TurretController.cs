using Core.Player.Scripts.Bullet;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Player.Scripts
{
    public class TurretController : MonoBehaviour
    {
        private const int BulletLifeTimeDelay = 700;

        [SerializeField] private BulletsPool bulletsPool;
        [SerializeField] private Transform bulletSpawnPoint;

        private BulletsPool _bulletsPoolInstance;

        private void Awake()
        {
            _bulletsPoolInstance = Instantiate(bulletsPool.gameObject).GetComponent<BulletsPool>();
            _bulletsPoolInstance.InitBullets();
        }

        public void ChangeRotate(float rotate)
            => transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.up * rotate);

        public void Shot()
        {
            var bullet = _bulletsPoolInstance.GetBullet();
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.transform.rotation = bulletSpawnPoint.rotation;
            bullet.SetActive(true);

            DeactivationBullet(bullet).Forget();
        }

        private async UniTask DeactivationBullet(GameObject bullet)
        {
            await UniTask.WhenAny(UniTask.Delay(BulletLifeTimeDelay), UniTask.WaitUntil(() => bullet == null || !bullet.activeSelf));

            if (bullet == null || !bullet.activeSelf)
                return;

            bullet.SetActive(false);
        }
    }
}