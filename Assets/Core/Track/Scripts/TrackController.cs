using System.Collections.Generic;
using System.Linq;
using Core.Enemy.Scripts;
using Core.Player.Scripts;
using UnityEngine;

namespace Core.Track.Scripts
{
    public class TrackController : MonoBehaviour
    {
        [SerializeField] private GameObject trackElementPrefab;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int trackSize;
        [SerializeField] private int enemiesPerTrackElement;

        private readonly List<GameObject> _trackElements = new();
        private readonly List<EnemyController> _enemyControllers = new();

        public Vector3 EndTrackPoint => _trackElements.Last().transform.position;

        public void GenerateTrack()
        {
            for (var i = 0; i < trackSize; i++)
            {
                var trackElement = Instantiate(trackElementPrefab, transform);
                var offsetZ = i * trackElement.GetComponent<TrackElement>().GetSizeZ();
                trackElement.transform.position = new Vector3(0f, 0f, offsetZ);
                _trackElements.Add(trackElement);
            }
        }

        public void GenerateEnemy()
        {
            foreach (var trackElement in _trackElements)
            {
                for (var i = 0; i < enemiesPerTrackElement; i++)
                {
                    var randomX = Random.Range(-10f, 10f);
                    var randomZ = Random.Range(-5f, 40f);

                    var randomPosition = trackElement.transform.position + new Vector3(randomX, 1, randomZ);

                    var enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
                    _enemyControllers.Add(enemy.GetComponent<EnemyController>());
                }
            }
        }

        public void StartGame(GameObject carController)
        {
            foreach (var enemyController in _enemyControllers)
                enemyController.StartGame(carController);
        }

        public void StopGame()
        {
            foreach (var enemyController in _enemyControllers.Where(enemyController => enemyController != null))
            {
                enemyController.StopGame();
                Destroy(enemyController.gameObject);
            }
        }
    }
}
