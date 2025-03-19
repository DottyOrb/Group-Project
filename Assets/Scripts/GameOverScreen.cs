using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void RetryButton() 
    {
        SceneManager.LoadScene("Group");
    }

    public void TitleButton() 
    {
        SceneManager.LoadScene("TitleMenu");
    }

    public void QuitButton() 
    { 
        Application.Quit();
    }
}
