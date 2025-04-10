using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Collections;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player instance;
    public float speed = 5.0f;
    public int playerHealth = 5;
    public TMP_Text healthText;
    public float knockbackForce = 2f;
    private Rigidbody2D rb;
    private SpriteRenderer _spriteRenderer;
    private Vector2 moveInput;
    public Sprite[] animationSprites;
    public HealthBar healthBarScript;
    private Animator animator;
    private bool canMove = true;
    public Coroutine IFramesRef;
    public bool canBeAttacked = true;
    public Score scoreScript;
    public int keys = 0; // Just a viusal cue for how many keys the player has collected
    public TMP_Text KeyText;


    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canMove == true)
        {
            rb.linearVelocity = moveInput * speed; // Moves player acording to inputs if canMove is true
        }

        HealthBar.instance.Current = playerHealth; // Updates the player health bar
        KeyText.text = "KEYS: " + keys.ToString(); // Updates the key text
    }

    public void Move(InputAction.CallbackContext context) // Animations
    {
        animator.SetBool("isWalking", true);

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }

        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyMelee") || other.gameObject.layer == LayerMask.NameToLayer("Obstacle") || other.gameObject.layer == LayerMask.NameToLayer("EnemyRanged") || other.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile") || other.gameObject.layer == LayerMask.NameToLayer("Spawner"))
        {
            Vector2 direction = (this.transform.position - other.transform.position).normalized;
            if (IFramesRef == null && canBeAttacked == true)
            {
                StartCoroutine(PlayerKnockback(direction, knockbackForce)); // Knocks player back
                playerHealth--;
                IFramesRef = StartCoroutine(IFrames()); // Starts the Invincibilty Frames
            }
            healthText.text = "HP: " + playerHealth.ToString();
            if (playerHealth <= 0)
            {
                SceneManager.LoadScene("GameOver"); // If the player health is 0, loads the game over scene
            }
        }
    }

    public IEnumerator PlayerKnockback(Vector2 direction, float knockbackForce) // Knocks player back when hit, also changes their color quickly for a visual cue
    {
        canMove = false;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.2f);

        rb.linearVelocity = Vector2.zero;
        canMove = true;
    }

    public IEnumerator IFrames() // Gives the Player Invinablity for 0.5 seconds after getting hit
    {
        canBeAttacked = false;
        _spriteRenderer.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.color = new Color(1, 1, 1, 1);
        canBeAttacked = true;
        IFramesRef = null;
    }
}
