using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
// tutorial used https://www.youtube.com/watch?v=UjqhsTce_L0&t=30s for part of the code
public class Weaponfollow : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;
    public PauseMenu pauseMenu;
    public Score scoreScript;
    

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

            Vector3 rotation = mousePos - transform.position;

            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }

       
        /*if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }




        }*/
        //if the button is pressed down then the bullet will fire
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (!PauseMenu.isPaused) 
            {
                //canFire = false;
                Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            }

        }




        Debug.Log(canFire);


    }

    private void shoot()
    {
        if (!canFire)
        {

        }
    }


   

   

}
