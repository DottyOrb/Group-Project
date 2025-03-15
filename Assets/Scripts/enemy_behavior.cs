using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class enemy_behavior : MonoBehaviour
{
    #region Public Variables
    public float speed; // Controls movement speed
    public float distanceBetween; // Distance for when Enemy stops moving if too close to player
    public float attackSpeed = 1f; // Cooldown time between Attacks
    public int attackPower; // How much damage the Enemy does
    public int health;
    public int defence;
    #endregion

    #region Private Variables
    private float distance; // The distance between the Enemy and Player
    private bool canAttack = true; // Controls Attack Delay
    NavMeshAgent agent;
    [SerializeField] Transform target;
    #endregion

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
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

        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (canAttack)
            {
                StartCoroutine(AttackCooldown());
                PlayerHealthPlaceholder.instance.DamagePlayer(attackPower);
            }
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;

        yield return new WaitForSeconds(attackSpeed);

        canAttack = true;
    }

    public void DamageEnemy(int damage)
    {
        int finalDamage = damage * (100 / defence);
        health -= finalDamage;
    }
}

// To Do List
// Do Enemy Attack  - Damages Player with a cooldown
// Make the Enemy be able to take Damage from the player
// Formula for Defence Stat  is Final Damage = Incoming Damage * (100/(100defence))

