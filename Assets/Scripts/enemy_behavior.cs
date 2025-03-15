using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class enemy_behavior : MonoBehaviour
{
    #region Public Variables
    public float speed;
    public float distanceBetween; // Distance for when Enemy stops moving if too close to player
    public float attackCooldown; // Cooldown time between Attacks
    public float attackPower; // How much damage the Enemy does
    #endregion

    #region Private Variables
    private float distance; // The distance between the Enemy and Player
    private bool inRangeForAttack; // Determines if the Enemy is close enough to attack Player
    private bool canAttack = true; // Controls Attack State
    private Coroutine attackCoroutine;
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
        //Debug.Log("Distance: "+distance+""+"InRangeForAttack: "+inRangeForAttack+""+"CanAttack+"+canAttack);

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
            inRangeForAttack = false;
        }
        else if (distance <= distanceBetween) // Checks whether the Enemy is close to the Player - Stops Movement, Enables Attack
        {
            inRangeForAttack = true;
        }

        // Attack Conditions
        if (inRangeForAttack && canAttack)
        {
            if (attackCoroutine == null) // Prevent multiple Coroutines from starting
            {
                canAttack = true;
                attackCoroutine = StartCoroutine(AttackPlayer());
            }
        }
        else if (!inRangeForAttack)
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }

    IEnumerator AttackPlayer()
    {
        canAttack = false;
        Debug.Log(("CoRoutine Started"));

        // Input Player Damage Script
        PlayerHealthPlaceholder.instance.DamagePlayer(5); // Player Damage Script

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        attackCoroutine = null; // Reset the coroutine reference
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Placeholder"))
        {
            // Im thinking of checking if the weapon collides with the enemy not the player
            // This is so that when the player walks into the enemy then the enemy wont get damaged unless the player attacks
            // for defence, ill use this formula - Final Damage = Incoming Damage * (100/(100defence))
        }
    }
}

// To Do List
// Do Enemy Attack  - Damages Player with a cooldown
// Make the Enemy be able to take Damage from the player