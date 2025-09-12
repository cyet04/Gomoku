using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public static PauseUI Instance { get; private set; }

    [SerializeField] private GameObject pausePanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenPausePanel()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        BoardManager.Instance.isPausedGame = true;
    }

    public void Replay()
    {

    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        BoardManager.Instance.isPausedGame = false;
    }

    public void Home()
    {

    }
}
