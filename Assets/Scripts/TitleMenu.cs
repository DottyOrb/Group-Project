using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public GameObject ControlPopUpObject;
    void Start()
    {
        ControlPopUpObject.SetActive(false); // Disables Control menu on start
    }
    public void StartButton() 
    {
        SceneManager.LoadScene("Group"); // Loads Game
    }
    public void OpenControls()
    {
        ControlPopUpObject.SetActive(true); // Enables Control menu
    }
    public void CloseControls()
    { 
        ControlPopUpObject.SetActive(false); // Disable Control Menu
    }
    public void QuitApplication() // Quit Applicaations
    {
        Application.Quit();
    }
}
