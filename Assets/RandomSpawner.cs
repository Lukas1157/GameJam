using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public Transform[] spawnPoints; // Array mit Spawnpunkten
    public GameObject[] enemyPrefabs; // Array mit Gegner-Prefabs
    public float minSpawnInterval = 1f; // Minimales Spawn-Intervall
    public float maxSpawnInterval = 3f; // Maximales Spawn-Intervall

    void Start()
    {
        // Coroutine starten
        StartCoroutine(SpawnEnemies());
    }

    // Coroutine zum Spawnen von Gegnern
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Zufälliges Intervall berechnen
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);

            // Warten bis zum nächsten Spawn
            yield return new WaitForSeconds(spawnInterval);

            // Zufälligen Gegner und Spawnpunkt auswählen
            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);

            // Gegner am zufälligen Punkt spawnen
            Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, Quaternion.identity);
        }
    }
}
