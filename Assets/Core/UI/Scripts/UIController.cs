using Core;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject levelCompleted;
    [SerializeField] private GameObject levelFailed;
    [SerializeField] private Slider mapSlider;

    private Transform _targetMapPoint;
    private Vector3 _endMapPosition;
    private float _startMapDistance;

    private void OnEnable()
    {
        restartButton.onClick.AddListener(() => GlobalGameEvents.RestartGame?.Invoke());
        startButton.onClick.AddListener(() => GlobalGameEvents.StartGame?.Invoke());
    }

    private void OnDisable()
    {
        restartButton.onClick.RemoveAllListeners();
        startButton.onClick.RemoveAllListeners();
    }

    private void Update()
    {
        if (!mapSlider.gameObject.activeSelf)
            return;

        MapSliderControlling();
    }

    public void InitMapSlider(Vector3 startPosition, Vector3 endPosition, Transform targetPoint)
    {
        mapSlider.value = 0;
        _startMapDistance = Vector3.Distance(startPosition, endPosition);
        _endMapPosition = endPosition;
        _targetMapPoint = targetPoint;
    }

    public void SetActiveMapSlider(bool isActive)
        => mapSlider.gameObject.SetActive(isActive);

    public void SetActiveRestart(bool isActive)
        => restartButton.gameObject.SetActive(isActive);

    public void SetActiveStart(bool isActive)
        => startButton.gameObject.SetActive(isActive);

    public void SetActiveLevelFailed(bool isActive)
        => levelFailed.gameObject.SetActive(isActive);

    public void SetActiveLevelCompleted(bool isActive)
        => levelCompleted.gameObject.SetActive(isActive);

    public void DeactivateAll()
    {
        SetActiveRestart(false);
        SetActiveStart(false);
        SetActiveLevelFailed(false);
        SetActiveLevelCompleted(false);
        SetActiveMapSlider(false);
    }

    private void MapSliderControlling()
    {
        var actualDistance = Vector3.Distance(_endMapPosition, _targetMapPoint.position);
        var sliderValue = 1 - actualDistance / _startMapDistance;

        mapSlider.value = sliderValue;
    }
}
