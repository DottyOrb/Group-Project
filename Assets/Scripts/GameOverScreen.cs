using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void RetryButton() 
    {
        SceneManager.LoadScene("Group"); // Restarts Level
    }

    public void TitleButton() 
    {
        SceneManager.LoadScene("TitleMenu"); // Returns to Title Screen
    }

    public void QuitButton() 
    { 
        Application.Quit(); // Quits Application
    }
}
