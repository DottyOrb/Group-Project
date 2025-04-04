using UnityEngine;
using System.Collections;

public class Ranged_Only_Spawner : MonoBehaviour
{
    #region Public Variables
    public GameObject RangedEnemyPrefab;
    public Transform target;

    public int minEnemiesPerBatch = 1;
    public int maxEnemiesPerBatch = 3;

    public float minSpawnInterval = 5f;
    public float maxSpawnInterval = 10f;

    public float distance;
    public float startSpawning = 10f;

    public float spawnRadius = 2f;

    public int spawnerHealth;

    public Coroutine SpawnEnemiesRef;
    #endregion
    private void Start()
    {
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
            float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(interval);

            int enemiesBaatchSize = Random.Range(minEnemiesPerBatch, maxEnemiesPerBatch);
            for (int i = 0; i < enemiesBaatchSize; i++)
            {
                Vector2 spawnOffset = Random.insideUnitCircle * spawnRadius;
                Vector2 spawnPosition = (Vector2)gameObject.transform.position + spawnOffset;

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
                Destroy(gameObject);
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
}
