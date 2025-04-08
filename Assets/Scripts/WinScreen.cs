using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public void titleButton() 
    {
        SceneManager.LoadScene("TitleMenu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
