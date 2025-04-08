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
    public int keys = 0;
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
            rb.linearVelocity = moveInput * speed;
        }

        HealthBar.instance.Current = playerHealth;
        KeyText.text = "KEYS: " + keys.ToString();
    }

    public void Move(InputAction.CallbackContext context)
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
                StartCoroutine(PlayerKnockback(direction, knockbackForce));
                playerHealth--;
                IFramesRef = StartCoroutine(IFrames());
            }
            healthText.text = "HP: " + playerHealth.ToString();
            if (playerHealth <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    public IEnumerator PlayerKnockback(Vector2 direction, float knockbackForce)
    {
        canMove = false;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.2f);

        rb.linearVelocity = Vector2.zero;
        canMove = true;
    }

    public IEnumerator IFrames()
    {
        canBeAttacked = false;
        _spriteRenderer.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.color = new Color(1, 1, 1, 1);
        canBeAttacked = true;
        IFramesRef = null;
    }
}
