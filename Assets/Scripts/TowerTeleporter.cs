using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTeleporter : MonoBehaviour
{
    [Header("Teleport Settings")]
public Transform[] spawnPoints; // Liste der Spawnpunkte (Empty GameObjects)
    public float minTeleportTime = 30f; // Minimale Zeit in Sekunden
    public float maxTeleportTime = 120f; // Maximale Zeit in Sekunden

    private void Start()
    {
        // Startet die Teleportation-Schleife
        StartCoroutine(TeleportRoutine());
    }

    private IEnumerator TeleportRoutine()
    {
        while (true)
        {
            // Wählt eine zufällige Zeit zwischen minTeleportTime und maxTeleportTime
            float waitTime = Random.Range(minTeleportTime, maxTeleportTime);
            yield return new WaitForSeconds(waitTime);

            // Wählt einen zufälligen Spawnpunkt aus
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Teleportiert den Turm zu diesem Punkt
            if (randomSpawnPoint != null)
            {
                transform.position = randomSpawnPoint.position;
                Debug.Log($"Turm teleportiert zu: {randomSpawnPoint.name}");
            }
            else
            {
                Debug.LogWarning("Kein gültiger Spawnpunkt gefunden!");
            }
        }
    }
}

