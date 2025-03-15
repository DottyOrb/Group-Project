using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// tutorial used https://www.youtube.com/watch?v=UjqhsTce_L0&t=30s for part of the code
public class Weaponfollow : MonoBehaviour
{
    public Transform player; 
    public float followSpeed = 5f; 
    public float rotationSpeed = 10f; 
    public bool flipOnOppositeDirection = true; 
    private Vector3 offset;
    private bool _bulletActive;
    public Projectile bulletPrefab;

    void Start()
    {
        offset = transform.position - player.position; 
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

        //if the button is pressed down then the bullet will fire
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();

        }



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


    private void Shoot()
    {
        if (!_bulletActive)
        {
            Projectile projectile = Instantiate(this.bulletPrefab, this.transform.position, Quaternion.identity);
            projectile.destroyed += BulletDestroyed;
            _bulletActive = true;
        }
    }

    private void BulletDestroyed()
    {
        _bulletActive = false;
    }


}
