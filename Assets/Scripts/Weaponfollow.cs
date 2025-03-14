using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// tutorial used https://www.youtube.com/watch?v=UjqhsTce_L0&t=30s
public class Weaponfollow : MonoBehaviour
{
    public Transform player; 
    public float followSpeed = 5f; 
    public float rotationSpeed = 10f; 
    public bool flipOnOppositeDirection = true; 
    private Vector3 offset; 

    void Start()
    {
        offset = transform.position - player.position; // Calculate initial offset
    }

    void Update()
    {

        // Follow the player smoothly
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Rotate towards the mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Flip the sword if needed
        if (flipOnOppositeDirection)
        {
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }


    }

}
