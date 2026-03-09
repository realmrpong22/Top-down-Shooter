using UnityEngine;
using TMPro;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [SerializeField] Wave[] waves;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] TextMeshProUGUI waveText;

    int currentWave = 0;
    int enemiesAlive = 0;

    void Start()
    {
        StartWave();
    }

    void StartWave()
    {
        if (currentWave >= waves.Length)
        {
            Debug.Log("All waves complete!");
            return;
        }

        waveText.text = "WAVE " + (currentWave + 1);
        waveText.gameObject.SetActive(true);

        StartCoroutine(SpawnWaveCoroutine());
    }

    IEnumerator SpawnWaveCoroutine()
    {
        yield return new WaitForSeconds(1f);

        Wave wave = waves[currentWave];

        foreach (EnemySpawn spawn in wave.enemies)
        {
            for (int i = 0; i < spawn.count; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                GameObject enemy = Instantiate(spawn.enemyPrefab, spawnPoint.position, Quaternion.identity);

                Health health = enemy.GetComponent<Health>();
                health.onDeath.AddListener(OnEnemyDeath);

                enemiesAlive++;
            }
        }

        currentWave++;
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