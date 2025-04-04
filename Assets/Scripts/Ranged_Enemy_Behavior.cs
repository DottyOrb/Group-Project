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
    public int defence; // Final Damage = Incoming Damage * (100/(100defence))
    public int EnemyScore;
    public int amountKilled;
    public Transform firingPoint;
    public float fireRate;
    public float enableShootingDistance;
    public GameObject bulletPrefab;
    public GameObject enemySpawner;
    public float knockbackForce = 2f;
    #endregion

    #region Private Variables
    private float distance; // The distance between the Enemy and Player
    //private bool canAttack = true; // Controls Attack Delay
    private float timeToFire;
    NavMeshAgent agent;
    [SerializeField] Transform target;
    private Rigidbody2D rb;
    #endregion

    private void Awake()
    {
        GetTarget();
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

        // Movement Conditions
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

    //void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        if (canAttack)
    //        {
    //            //Attack Script was here
    //            PlayerHealthPlaceholder.instance.DamagePlayer(attackPower);
    //        }
    //    }
    //}

    //public void DamageEnemy(int damage)
    //{
    //    int finalDamage = damage * (100 / defence);
    //    health -= finalDamage;
    //}

    private void EnemyKilled()
    {
        this.amountKilled++;
        if (this.amountKilled >= 100)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Sword") || other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            Vector2 direction = (this.transform.position - other.transform.position).normalized;
            StartCoroutine(enemyKnockback(direction, knockbackForce));

            health--;

            int spawnEnemySpawner = Random.Range(0, 16);
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
        }
    }

    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public IEnumerator enemyKnockback(Vector2 direction, float knockbackForce)
    {
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.6f);

        rb.linearVelocity = Vector2.zero;
    }
}
