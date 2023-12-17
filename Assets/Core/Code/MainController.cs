using System;
using Core.Car.Scripts;
using Core.Code.Camera;
using Core.Track.Scripts;
using UnityEngine;

namespace Core.Code
{
    public class MainController : MonoBehaviour
    {
        [SerializeField] private GameObject trackControllerPrefab;
        [SerializeField] private GameObject playerControllerPrefab;
        [SerializeField] private CameraController cameraController;
        
        private TrackController _trackController;
        private PlayerController _playerController;

        private void Start()
        {
            _trackController = Instantiate(trackControllerPrefab).GetComponent<TrackController>();
            _trackController.GenerateTrack();

            _playerController = Instantiate(playerControllerPrefab).GetComponent<PlayerController>();
            _playerController.Init();
            cameraController.Init(_playerController.CarTransform);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                StartGame();
            }
        }

        private async void StartGame()
        {
            await cameraController.StartGameCameraPositionAnimation();
            _playerController.StartMove();
        }
    }
}