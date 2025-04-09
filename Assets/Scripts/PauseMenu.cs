using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;
    public GameObject PauseMenuObject;
    public GameObject ControlPopUpObject;

    private void Start()
    {
        ControlPopUpObject.SetActive(false); // Deactivates Control Menu popup at the start
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Pauses the game
        {
            isPaused = !isPaused;
            PauseToggle();
        }
    }

    void PauseToggle() // Stops Game Time
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

    public void ResumeButton() // Resumes Game Time
    {
        isPaused = !isPaused;
        PauseToggle();
    }

    public void OpenControls()  // Enables Control Screen
    {
        ControlPopUpObject.SetActive(true);
    }
    
    public void CloseControls() // Closes Control Menu 
    {
        ControlPopUpObject.SetActive(false);
    }

    public void TitleButton() // Returns player to tiles screen
    {
        isPaused = !isPaused;
        PauseToggle();
        SceneManager.LoadScene("TitleMenu");
    }

    public void QuitButton() // Quits Application
    {
        Application.Quit();
    }
}
