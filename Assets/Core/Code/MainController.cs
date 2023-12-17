using System;
using Core.Car.Scripts;
using Core.Track.Scripts;
using UnityEngine;

namespace Core.Code
{
    public class MainController : MonoBehaviour
    {
        [SerializeField] private GameObject trackControllerPrefab;
        [SerializeField] private GameObject playerControllerPrefab;
        
        private TrackController _trackController;
        private PlayerController _playerController;

        private void Start()
        {
            _trackController = Instantiate(trackControllerPrefab).GetComponent<TrackController>();
            _trackController.GenerateTrack();

            _playerController = Instantiate(playerControllerPrefab).GetComponent<PlayerController>();
            _playerController.Init();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                StartGame();
            }
        }

        private void StartGame()
        {
            _playerController.StartMove();
        }
    }
}