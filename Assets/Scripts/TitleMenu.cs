using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public void StartButton() 
    {
        SceneManager.LoadScene("Group");
    }

    public void QuitApplication() 
    {
        Application.Quit();
    }
}
