using UnityEngine;
using UnityEngine.SceneManagement;

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
        SoundManager.Instance.PlaySound(SoundManager.Instance.click);
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        BoardManager.Instance.isPausedGame = true;
    }

    public void Replay()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.click);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.click);
        if (!BoardManager.Instance.isGameOver)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            BoardManager.Instance.isPausedGame = false;
        }
    }

    public void Home()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.click);
        SceneManager.LoadScene(0);
    }
}
