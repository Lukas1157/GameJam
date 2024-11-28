using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hebel_Fahrstuhl : MonoBehaviour
{
    public Fahrstuhl_Movement platformScript; // Verbindung zum Plattformskript
    private bool isActivated = false; // Status des Schalters
    public float rotationAngle = 45f; // Winkel der Rotation
    private float initialZRotation; // Ausgangsrotation des Schalters

    private void Start()
    {
        // Speichere die Z-Rotation des Schalters
        initialZRotation = transform.eulerAngles.z;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.CompareTag("Sword")) // Reagiert nur auf den Spieler
        {
            ToggleSwitch(); // Schalter umschalten
            platformScript.ActivatePlatform(); // Plattform aktivieren
              Debug.Log("Spieler hat den Schalter berührt.");
        }
    }

    private void ToggleSwitch()
    {
        if (isActivated)
        {
            transform.rotation = Quaternion.Euler(0, 0, initialZRotation); // Zurückdrehen
            isActivated = false;
          
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, initialZRotation + rotationAngle); // Drehen
            isActivated = true;
           
        }
    }
}