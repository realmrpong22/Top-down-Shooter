using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform[] spawnPoints;

    int waveNumber = 0;
    int enemiesAlive = 0;

    void Start()
    {
        StartWave();
    }

    void StartWave()
    {
        waveNumber++;

        int enemiesToSpawn = 3 + waveNumber * 2;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject enemy = Instantiate(enemyPrefab, spawn.position, Quaternion.identity);

            Health health = enemy.GetComponent<Health>();
            health.onDeath.AddListener(OnEnemyDeath);

            enemiesAlive++;
        }

        Debug.Log("Wave " + waveNumber + " started with " + enemiesToSpawn + " enemies.");
    }

    void OnEnemyDeath()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            Invoke(nameof(StartWave), 2f);
        }
    }
}