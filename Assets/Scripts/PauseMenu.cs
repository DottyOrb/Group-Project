using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;
    public GameObject PauseMenuObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            PauseToggle();
        }
    }

    void PauseToggle()
    {
        if (isPaused)
        {
            Time.timeScale = 0.0f;
            PauseMenuObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            PauseMenuObject.SetActive(false);
        }
    }

    public void ResumeButton()
    {
        isPaused = !isPaused;
        PauseToggle();
    }

    public void TitleButton()
    {
        isPaused = !isPaused;
        PauseToggle();
        SceneManager.LoadScene("TitleMenu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
