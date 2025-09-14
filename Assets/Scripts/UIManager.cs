using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void Play()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.click);
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.click);
        Application.Quit();
    }
}
