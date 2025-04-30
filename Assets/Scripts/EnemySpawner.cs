using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] bossPrefabs;
    public SpriteRenderer mapSpriteRenderer;

    private bool canSpawn = false;

    void OnEnable()
    {
        WaveManager.WaveStarted += OnWaveStarted;
        WaveManager.WaveEnded += OnWaveEnded;
    }

    void OnDisable()
    {
        WaveManager.WaveStarted -= OnWaveStarted;
        WaveManager.WaveEnded -= OnWaveEnded;
    }

    void OnWaveStarted(int waveNumber, float speedMultiplier, float damageMultiplier,float healthMultipler, float spawnDelay)
    {
        canSpawn = true;
        if (waveNumber % 5 == 0 && waveNumber != 0) // Spawn boss every 5 waves
            {
                SpawnBossEnemy();
            }
        StartCoroutine(SpawnEnemies(spawnDelay));
    }

    void OnWaveEnded(int waveNumber)
    {
        canSpawn = false;
        StopAllCoroutines(); 
    }

    private IEnumerator SpawnEnemies(float timer)
    {
        while (canSpawn)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timer); 
        }
    }

    void SpawnEnemy()
    {
        Vector2 min = mapSpriteRenderer.bounds.min;
        Vector2 max = mapSpriteRenderer.bounds.max;

        float randomX = Random.Range(min.x, max.x);
        float randomY = Random.Range(min.y, max.y);

        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);
        int enemyType = Random.Range(0, enemyPrefabs.Length);

        Instantiate(enemyPrefabs[enemyType], spawnPosition, Quaternion.identity);
    }
    void SpawnBossEnemy()
    {
        Vector2 min = mapSpriteRenderer.bounds.min;
        Vector2 max = mapSpriteRenderer.bounds.max;

        float randomX = Random.Range(min.x, max.x);
        float randomY = Random.Range(min.y, max.y);

        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);
        int enemyType = Random.Range(0, bossPrefabs.Length);

        Instantiate(bossPrefabs[enemyType], spawnPosition, Quaternion.identity);
    }
}
