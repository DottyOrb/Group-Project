using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public GameObject keyOne;
    public GameObject keyTwo;
    public bool keyOneDead = false;
    public bool keyTwoDead = false;

    public void Update()
    {
        if (keyOne == null && keyOneDead == false)
        {
            Player.instance.keys++;
            keyOneDead = true;
        }
        if (keyTwo == null && keyTwoDead == false)
        {
            Player.instance.keys++;
            keyTwoDead = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (keyOne == null && keyTwo == null)
        {
            SceneManager.LoadScene("WinScene");
        }
    }
}
