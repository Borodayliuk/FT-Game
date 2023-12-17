using System;
using Core.Car.Scripts;
using Core.Code.Camera;
using Core.Track.Scripts;
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
            GlobalGameEvents.GameLoss += OnGameLoss;
            GlobalGameEvents.LevelCompleted += OnLevelCompleted;
        }

        private void UnsubscribeEvents()
        {
            GlobalGameEvents.StartGame -= OnStartGame;
            GlobalGameEvents.RestartGame -= OnRestartGame;
            GlobalGameEvents.GameLoss -= OnGameLoss;
            GlobalGameEvents.LevelCompleted -= OnLevelCompleted;
        }

        private void Init()
        {
            _trackController = Instantiate(trackControllerPrefab).GetComponent<TrackController>();
            _trackController.GenerateTrack();
            _playerController = Instantiate(playerControllerPrefab).GetComponent<PlayerController>();

            _playerController.Init();
            InitInternal();
        }

        private void OnStartGame()
        {
            uiController.DeactivateAll();

            StartGame();
        }

        private void OnRestartGame()
            => InitInternal();

        private void OnGameLoss()
        {
            _playerController.StopMove();

            uiController.DeactivateAll();
            uiController.SetActiveRestart(true);
            uiController.SetActiveLevelCompleted(true);
            _trackController.StopGame();
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
            _trackController.GenerateEnemy();
        }

        private async void StartGame()
        {
            await cameraController.StartGameCameraPositionAnimation();
            _playerController.StartGame();
            _trackController.StartGame();
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}