using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
     public GameObject Player; // Spielerobjekt (HeroKnight)
    public GameObject SpawnPoint; // Spawnpunkt
    public float slowSpeed = 0.5f; // Verlangsamte Geschwindigkeit
    public float slowDuration = 3f;
    public float slowJump = 2f;// Dauer der Verlangsamung in Sekunden

    private HeroKnight heroKnightScript; // Zugriff auf das HeroKnight-Skript
    private float originalSpeed;
    private float originalJump;// Speichert die ursprüngliche Geschwindigkeit

    private void Start()
{
    heroKnightScript = Player.GetComponent<HeroKnight>();
    if (heroKnightScript != null)
    {
        originalSpeed = heroKnightScript.Speed;
        originalJump = heroKnightScript.JumpForce;// Zugriff über die Eigenschaft
    }
    else
    {
        Debug.LogError("HeroKnight-Skript nicht gefunden!");
    }
}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Teleportiere den Spieler zum Spawnpunkt
            Player.transform.position = SpawnPoint.transform.position;

            // Verlangsamen
            if (heroKnightScript != null)
            {
                StartCoroutine(SlowPlayer());
            }
        }
    }

    private IEnumerator SlowPlayer()
{
    heroKnightScript.Speed = slowSpeed;
    heroKnightScript.JumpForce = slowJump;// Verlangsamen
  

    yield return new WaitForSeconds(slowDuration);

    heroKnightScript.Speed = originalSpeed;
    heroKnightScript.JumpForce = originalJump;// Zurücksetzen

}
}
