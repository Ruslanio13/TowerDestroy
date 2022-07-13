using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private GameObject _pauseObjects;

    private void Start()
    {
        _pauseButton.onClick.AddListener(() => SetPauseCondition());
        _resumeButton.onClick.AddListener(() => ResumeGame());
        _restartButton.onClick.AddListener(() => RestartGame());
    }

    private void SetPauseCondition()
    {
        Time.timeScale = 0;
        _pauseObjects.gameObject.SetActive(true);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        _pauseObjects.gameObject.SetActive(false);
    }

    private void RestartGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
