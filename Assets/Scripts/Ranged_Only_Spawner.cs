using UnityEngine;
using System.Collections;
using UnityEngine.U2D;

public class Ranged_Only_Spawner : MonoBehaviour
{
    #region Public Variables
    public GameObject RangedEnemyPrefab;
    public Transform target;

    public int minEnemiesPerBatch = 1;
    public int maxEnemiesPerBatch = 3;

    public float minSpawnInterval = 5f;
    public float maxSpawnInterval = 10f;

    public float distance; // Distance between the player and spawner
    public float startSpawning = 10f; //using the variable above, the spawner checks if the player is close enough to it

    public float spawnRadius = 2f; // Circle Radius around the spawner

    public int spawnerHealth;

    public int SpawnerScore = 15;

    public Coroutine SpawnEnemiesRef;

    SpriteRenderer sprite;
    #endregion
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        GetTarget();
    }
    private void Update()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance <= startSpawning)
        {
            if (SpawnEnemiesRef == null)
            {
                SpawnEnemiesRef = StartCoroutine(SpawnEnemies());
            }
        }
        else
        {
            if (SpawnEnemiesRef != null)
            {
                StopAllCoroutines();
                SpawnEnemiesRef = null;
            }
        }
    }
    public IEnumerator SpawnEnemies()
    {
        while (true)
        {
            float interval = Random.Range(minSpawnInterval, maxSpawnInterval); // Decides when to spawn enemies in
            yield return new WaitForSeconds(interval);

            int enemiesBaatchSize = Random.Range(minEnemiesPerBatch, maxEnemiesPerBatch); // Decides how many enemies to spawn
            for (int i = 0; i < enemiesBaatchSize; i++)
            {
                Vector2 spawnOffset = Random.insideUnitCircle * spawnRadius;
                Vector2 spawnPosition = (Vector2)gameObject.transform.position + spawnOffset; // Decides what position to spawn enemies in

                Instantiate(RangedEnemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Sword") || other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            spawnerHealth--;
            if (spawnerHealth <= 0)
            {
                Score.Instance.AddToScore(SpawnerScore);
                Destroy(gameObject);
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
    public IEnumerator ChangeColour() // Changes the sprite color when they are hit to show a visual cue for the player
    {
        sprite.color = new Color(1, 0, 0, 1);

        yield return new WaitForSeconds(0.2f);

        sprite.color = new Color(1, 1, 1, 1);
    }
}
