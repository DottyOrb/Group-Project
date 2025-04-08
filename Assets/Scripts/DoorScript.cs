using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject keyOne;
    public GameObject keyTwo;
    public GameObject door;
    public GameObject finish;

    private void Start()
    {
        door.SetActive(false);
    }
    void Update()
    {
        if (keyOne == null && keyTwo == null)
        {
            finish.SetActive(true);
            door.SetActive(false);
        }
    }
}
