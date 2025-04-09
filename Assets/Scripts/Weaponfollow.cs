using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public ProgressBar progressBar;
    public Player player;
    public Coroutine ShootingActiveRef;


    void Start()
    {

        canFire = false;
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

        if (Input.GetMouseButtonDown(0) && canFire)
        {
            
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }

        if (scoreScript.score >= 100)
        {
            canFire = true;
            if (ShootingActiveRef == null)
            {
                ShootingActiveRef = StartCoroutine(ShootingActive(10));
            }
        }
        else
        {
            if (ShootingActiveRef != null)
            { 
                StopAllCoroutines();
                ShootingActiveRef = null;
            }
        }

        
        if (scoreScript.score == 0)
        {
           canFire = false;
        }
    }

    private IEnumerator ShootingActive(float delay)
    {
        Player.instance.playerHealth = 5;
        yield return new WaitForSeconds(delay);
        scoreScript.score = 0;
    }
}
