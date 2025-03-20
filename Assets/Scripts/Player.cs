using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;

    public int playerHealth = 3;

    public TMP_Text healthText;


   

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))

        {
            this.transform.position += Vector3.up * this.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position += Vector3.down * this.speed * Time.deltaTime;
        }


    }

  



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyMelee") || other.gameObject.layer == LayerMask.NameToLayer("Obstacle") || other.gameObject.layer == LayerMask.NameToLayer("EnemyRanged") || other.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))
        {
            playerHealth--;
            healthText.text = "HP: " + playerHealth.ToString();
            if (playerHealth <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
;
        }
    } 
}
