using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hebel_Fahrstuhl : MonoBehaviour
{

    public Fahrstuhl_Movement platformScript; // Verknüpfen mit dem Plattformskript
    private bool isActivated = false; // Status des Schalters
    public float rotationAngle = 45f; // Winkel, um den der Schalter gedreht wird
    private float initialZRotation; // Ausgangsrotation des Schalters

    private void Start()
    {
        // Speichere die Z-Rotation des Schalters
        initialZRotation = transform.eulerAngles.z;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword")) // Nur auf Spieler-Kollision reagieren
        {
            platformScript.ActivatePlatform(); // Plattform aktivieren
            ToggleSwitch();
        }
    }



    private void ToggleSwitch()
    {
        if (isActivated)
        {
            // Zurück zur Ausgangsrotation
            transform.rotation = Quaternion.Euler(0, 0, initialZRotation);
            isActivated = false;
        }
        else
        {
            // Drehe den Schalter um den Winkel
            transform.rotation = Quaternion.Euler(0, 0, initialZRotation + rotationAngle);
            isActivated = true;
        }
    }
}
