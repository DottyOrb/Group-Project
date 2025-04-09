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
            Player.instance.keys++; // Adds 1 to the keys variable
            keyOneDead = true;
        }
        if (keyTwo == null && keyTwoDead == false)
        {
            Player.instance.keys++; // Adds 1 to the keys variable
            keyTwoDead = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (keyOne == null && keyTwo == null) // The keys are destroyed when the player interacts with them, when both keys are destroyed the player can go up to the finish line and win
        {
            SceneManager.LoadScene("WinScene");
        }
    }
}
