using System;
using Core.Code.Camera;
using Core.Player.Scripts;
using Core.Track.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Code
{
    public class MainController : MonoBehaviour, IDisposable
    {
        [SerializeField] private UIController uiController;
        [SerializeField] private GameObject trackControllerPrefab;
        [SerializeField] private GameObject playerControllerPrefab;
        [SerializeField] private CameraController cameraController;

        private TrackController _trackController;
        private PlayerController _playerController;

        private void Start()
        {
            Init();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GlobalGameEvents.StartGame += OnStartGame;
            GlobalGameEvents.RestartGame += OnRestartGame;
            GlobalGameEvents.GameFailed += OnGameFailed;
            GlobalGameEvents.LevelCompleted += OnLevelCompleted;
        }

        private void UnsubscribeEvents()
        {
            GlobalGameEvents.StartGame -= OnStartGame;
            GlobalGameEvents.RestartGame -= OnRestartGame;
            GlobalGameEvents.GameFailed -= OnGameFailed;
            GlobalGameEvents.LevelCompleted -= OnLevelCompleted;
        }

        private void Init()
        {
            _trackController = Instantiate(trackControllerPrefab).GetComponent<TrackController>();
            _trackController.GenerateTrack();
            _playerController = Instantiate(playerControllerPrefab).GetComponent<PlayerController>();

            _playerController.Init(_trackController.EndTrackPoint);
            InitInternal();
        }

        private void OnStartGame()
        {
            uiController.DeactivateAll();

            StartGame().Forget();
        }

        private void OnRestartGame()
            => InitInternal();

        private void OnGameFailed()
        {
            Debug.LogError("Failed");
            _trackController.StopGame();

            _playerController.StopMove();
            _playerController.GameFailed();

            uiController.DeactivateAll();
            uiController.SetActiveRestart(true);
            uiController.SetActiveLevelCompleted(true);
        }

        private void OnLevelCompleted()
        {
            _playerController.StopMove();

            uiController.DeactivateAll();
            uiController.SetActiveRestart(true);
            uiController.SetActiveLevelFailed(true);
        }

        private void InitInternal()
        {
            _playerController.ReInit();
            cameraController.Init(_playerController.CarTransform);
            uiController.DeactivateAll();
            uiController.SetActiveStart(true);
            uiController.InitMapSlider(_playerController.CarTransform.position, _trackController.EndTrackPoint, _playerController.CarTransform);
            _trackController.GenerateEnemy();
        }

        private async UniTask StartGame()
        {
            await cameraController.StartGameCameraPositionAnimation();

            _playerController.StartGame();
            _trackController.StartGame();
            uiController.SetActiveMapSlider(true);
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}