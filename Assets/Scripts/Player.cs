using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Collections;
using System.Collections;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;

    public int playerHealth = 5;

    public TMP_Text healthText;

    public float knockbackForce = 2f;

    private Rigidbody2D rb;

    private SpriteRenderer spriteRenderer;

    public Sprite[] animationSprites;

    public HealthBar healthBarScript;
    private Animator animator;
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("InputX", -1);
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("InputX", 1);
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))

        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("InputY", 1);
            this.transform.position += Vector3.up * this.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("InputY", -1);
            this.transform.position += Vector3.down * this.speed * Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)) 
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", animator.GetFloat("InputX"));
            animator.SetFloat("InputX", 0);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputY", animator.GetFloat("InputY"));
            animator.SetFloat("InputY", 0);
        }

        healthBarScript.Current = playerHealth;


    }

  



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyMelee") || other.gameObject.layer == LayerMask.NameToLayer("Obstacle") || other.gameObject.layer == LayerMask.NameToLayer("EnemyRanged") || other.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))
        {
            Vector2 direction = (this.transform.position - other.transform.position).normalized;
            StartCoroutine(PlayerKnockback(direction, knockbackForce));

            playerHealth--;
            healthText.text = "HP: " + playerHealth.ToString();
            if (playerHealth <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
;
        }
    }

    public IEnumerator PlayerKnockback(Vector2 direction, float knockbackForce)
    {
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(1);

        rb.linearVelocity = Vector2.zero;
    }
}
