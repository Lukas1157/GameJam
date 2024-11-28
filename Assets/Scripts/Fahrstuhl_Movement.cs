using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fahrstuhl_Movement : MonoBehaviour
{
    public List<Transform> points; // Liste der Zielpunkte
    private int goalPoint = 1; // Zielpunkt beginnt bei Punkt 1
    public float moveSpeed = 2; // Bewegungsgeschwindigkeit
    public Transform platform; // Plattform-Transform
    private bool isActive = false; // Plattform aktiv/inaktiv

    private void Start()
    {
        // Setze die Plattform an die Position des ersten Punktes
        platform.position = points[0].position;

        // Zielpunkt auf den nächsten Punkt setzen
        goalPoint = 1;
    }

    private void Update()
    {
        if (isActive)
        {
            MoveToNextPoint();
        }
    }

    public void ActivatePlatform()
    {
        if (!isActive) // Nur aktivieren, wenn die Plattform aktuell nicht aktiv ist
        {
            isActive = true;
            Debug.Log("Plattform aktiviert und startet die Bewegung!");
        }
    }

    private void MoveToNextPoint()
    {
        // Bewege die Plattform zum aktuellen Zielpunkt
        platform.position = Vector2.MoveTowards(platform.position, points[goalPoint].position, moveSpeed * Time.deltaTime);

        // Wenn der Punkt erreicht wird
        if (Vector2.Distance(platform.position, points[goalPoint].position) < 0.1f)
        {
            Debug.Log($"Punkt {goalPoint} erreicht. Plattform stoppt.");
            isActive = false; // Plattform anhalten
            UpdateGoalPoint(); // Nächsten Punkt setzen
        }
    }

    private void UpdateGoalPoint()
    {
        // Berechne den nächsten Zielpunkt
        if (goalPoint == points.Count - 1)
        {
            goalPoint = 0; // Zurück zum ersten Punkt
        }
        else
        {
            goalPoint++; // Weiter zum nächsten Punkt
        }
    }
}