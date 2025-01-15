using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public Transform[] spawnPoints; // Array mit Spawnpunkten
    public GameObject[] enemyPrefabs; // Array mit Gegner-Prefabs

    [System.Serializable]
    public class Wave
    {
        public int enemyCount; // Anzahl der Gegner in dieser Welle
        public float spawnInterval; // Zeit zwischen Spawns in dieser Welle
    }

    public Wave[] waves; // Array mit allen Wellen
    public float wavePauseDuration = 5f; // Dauer der Pause zwischen den Wellen

    private int currentWaveIndex = 0; // Aktuelle Wellen-ID
    private bool isSpawningWave = false; // Ob eine Welle gerade gespawnt wird

    void Start()
    {
        StartCoroutine(HandleWaves());
    }

    // Coroutine für Wellen-Management
    IEnumerator HandleWaves()
    {
        while (currentWaveIndex < waves.Length)
        {
            if (!isSpawningWave)
            {
                // Welle initialisieren und starten
                yield return StartCoroutine(SpawnWave(waves[currentWaveIndex]));

                // Warten bis zur nächsten Welle
                Debug.Log($"Welle {currentWaveIndex + 1} abgeschlossen. Pause von {wavePauseDuration} Sekunden.");
                yield return new WaitForSeconds(wavePauseDuration);

                currentWaveIndex++; // Nächste Welle vorbereiten
            }

            yield return null;
        }

        Debug.Log("Alle Wellen abgeschlossen!");
    }

    // Coroutine für das Spawnen einer Welle
    IEnumerator SpawnWave(Wave wave)
    {
        isSpawningWave = true;

        Debug.Log($"Welle {currentWaveIndex + 1} startet mit {wave.enemyCount} Gegnern!");

        for (int i = 0; i < wave.enemyCount; i++)
        {
            // Zufälligen Gegner und Spawnpunkt auswählen
            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);

            // Gegner spawnen
            Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, Quaternion.identity);

            // Warten bis zum nächsten Spawn
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        isSpawningWave = false;
    }
}
