using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEditor;
using UnityEngine.XR;

public class Ranged_Enemy_Behavior : MonoBehaviour
{
    #region Public Variables
    public float speed; // Controls movement speed
    public float distanceBetween; // Distance for when Enemy stops moving if too close to player
    public int attackPower; // How much damage the Enemy does
    public int health;
    public int EnemyScore;
    public int amountKilled;
    public Transform firingPoint;
    public float fireRate;
    public float enableShootingDistance;
    public GameObject bulletPrefab;
    public GameObject enemySpawner;
    public float knockbackForce = 2f;
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;
    #endregion

    #region Private Variables
    private float distance; // The distance between the Enemy and Player
    private float timeToFire;
    NavMeshAgent agent;
    [SerializeField] Transform target;
    private Rigidbody2D rb;
    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;
    #endregion

    private void Awake()
    {
        GetTarget();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        timeToFire = 0f;
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Movement Conditions - Leszek D
        if (distanceBetween < distance) // Checks whether the Enemy is far away from the Player - Starts Movement, Disables Attack
        {
            agent.SetDestination(target.position);
            agent.speed = speed;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }

        Shoot();
    }

    private void Shoot()
    {
        if (timeToFire <= 0f)
        {
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            timeToFire = fireRate;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }


    private void EnemyKilled()
    {
        this.amountKilled++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Sword") || other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            Vector2 direction = (this.transform.position - other.transform.position).normalized;
            StartCoroutine(enemyKnockback(direction, knockbackForce)); // Knocks Enemy Back

            health--;

            int spawnEnemySpawner = Random.Range(0, 16); // Decides whether to spawn a spawner or not
            if (health <= 0)
            {
                if (spawnEnemySpawner <=14)
                {
                    Score.Instance.AddToScore(EnemyScore);
                    Destroy(gameObject);
                }
                else if (spawnEnemySpawner == 15)
                {
                    Score.Instance.AddToScore(EnemyScore);
                    Destroy(gameObject);
                    Vector2 spawnPosition = (Vector2)gameObject.transform.position;
                    Instantiate(enemySpawner, spawnPosition, Quaternion.identity);
                }
            }
            StartCoroutine(ChangeColour());
        }
    }

    private void GetTarget() // Finds game object with the player tag and asigns them as the target
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public IEnumerator enemyKnockback(Vector2 direction, float knockbackForce) // Knocks Enemy Back
    {
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.6f);

        rb.linearVelocity = Vector2.zero;
    }


    private void AnimateSprite()
    {
        _animationFrame++;

        if (_animationFrame >= animationSprites.Length)
        {
            _animationFrame = 0;
        }
        _spriteRenderer.sprite = this.animationSprites[_animationFrame];
    }
    public IEnumerator ChangeColour() // Changes the sprite color when they are hit to show a visual cue for the player
    {
        _spriteRenderer.color = new Color(1, 0, 0, 1);

        yield return new WaitForSeconds(0.2f);

        _spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
