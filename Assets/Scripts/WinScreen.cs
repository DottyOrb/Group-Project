using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public void titleButton() 
    {
        SceneManager.LoadScene("TitleMenu"); // Loads Title Menu
    }

    public void QuitButton()
    {
        Application.Quit(); // Quits Application
    }
}
