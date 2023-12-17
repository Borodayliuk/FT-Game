using Core;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject levelCompleted;
    [SerializeField] private GameObject levelFailed;

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
    }
}
