using UnityEngine;
using Unity.Collections;
using System.Collections;
public class Enemy_Spawner : MonoBehaviour
{
    #region Public Variables
    public GameObject MeleeEnemyPrefab;
    public GameObject RangedEnemyPrefab;

    public int minEnemiesPerBatch = 2;
    public int maxEnemiesPerBatch = 4;

    public float minSpawnInterval = 5f;
    public float maxSpawnInterval = 10f;

    public float spawnRadius = 2f;

    public int spawnerHealth = 8;
    #endregion

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
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

                int chooseEnemyType = Random.Range(0, 5);
                if (chooseEnemyType >= 0 && chooseEnemyType <= 3)
                {
                    Instantiate(MeleeEnemyPrefab, spawnPosition, Quaternion.identity);
                }
                else if (chooseEnemyType == 4)
                {
                    Instantiate(RangedEnemyPrefab, spawnPosition, Quaternion.identity);
                }
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
}
