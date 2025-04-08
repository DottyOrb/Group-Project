using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public GameObject ControlPopUpObject;
    void Start()
    {
        ControlPopUpObject.SetActive(false);
    }
    public void StartButton() 
    {
        SceneManager.LoadScene("Group");
    }
    public void OpenControls()
    {
        ControlPopUpObject.SetActive(true);
    }
    public void CloseControls()
    { 
        ControlPopUpObject.SetActive(false);
    }
    public void QuitApplication()
    {
        Application.Quit();
    }
}
