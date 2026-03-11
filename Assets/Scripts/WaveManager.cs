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

    public void StartWave()
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

        enemiesAlive = 0;

        foreach (EnemySpawn spawn in wave.enemies)
        {
            for (int i = 0; i < spawn.count; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                GameObject enemy = Instantiate(spawn.enemyPrefab, spawnPoint.position, Quaternion.identity);

                Health health = enemy.GetComponent<Health>();
                health.onDeath.AddListener(() => OnEnemyDeath(enemy));

                enemiesAlive++;
            }
        }
    }

    void OnEnemyDeath(GameObject enemy)
    {
        if (!enemy.CompareTag("Enemy"))
            return;

        enemiesAlive--;

        Debug.Log("Enemy died. Remaining: " + enemiesAlive);

        if (enemiesAlive <= 0)
        {
            currentWave++;
            UpgradeUI upgradeUI = FindObjectOfType<UpgradeUI>();

            if (upgradeUI != null)
            {
                upgradeUI.ShowUpgrades();
            }
            else
            {
                Debug.LogError("UpgradeUI not found in scene!");
            }
        }
    }
}