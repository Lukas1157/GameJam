using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fahrstuhl_Movement : MonoBehaviour
{
    public List<Transform> points;
    int goalPoint = 0;
    public float moveSpeed = 2;
    public Transform platform;

    private bool isActive = false;

    private void Update()
    {

        if (isActive)
        {
            MoveToNextPoint();
        }

    }

    public void ActivatePlatform()
    {
        if (!isActive)
        {
            isActive = true;
        }
    }

    void MoveToNextPoint()
    {

        platform.position = Vector2.MoveTowards(platform.position, points[goalPoint].position, 1 * Time.deltaTime * moveSpeed);

        if (Vector2.Distance(platform.position, points[goalPoint].position) < 0.1f)
        {
            isActive = false; // Plattform anhalten
            UpdateGoalPoint();
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
