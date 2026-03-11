using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;

    float spawnCountdown;
    [SerializeField] int enemiesToSpawn = 10;

    void Awake()
    {
        ResetSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        spawnCountdown -= Time.deltaTime;

        if (spawnCountdown <= 0 && enemiesToSpawn > 0)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            enemiesToSpawn--;
            ResetSpawn();
        }
    }

    void ResetSpawn()
    {
        spawnCountdown = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
