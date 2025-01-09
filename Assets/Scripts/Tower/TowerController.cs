using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TowerController : MonoBehaviour
{
    public int maxHealth = 10; // Maximale Lebenspunkte des Turms
    public int currentHealth; // Aktuelle Lebenspunkte
    public TowerHealthbar healthBar; // UI-Slider f�r die Lebenspunkte (falls ben�tigt)

    public Volume globalVolume; // Referenz zum Global Volume
    private Vignette vignetteEffect; // Referenz zum Vignette Effekt

    void Start()
    {
        currentHealth = maxHealth; // Setzt die Lebenspunkte auf das Maximum
        healthBar.SetMaxHealth(maxHealth); // Initialisiert den Lebensbalken

        // Zugriff auf den Vignetten-Effekt im Global Volume
        if (globalVolume.profile.TryGet(out vignetteEffect))
        {
            vignetteEffect.intensity.overrideState = true; // Aktiviert die Steuerung über den Code
            vignetteEffect.intensity.value = 0f; // Startet mit keiner Vignette
        }
        else
        {
            Debug.LogError("Vignette effect not found in the Global Volume!");
        }
    }

    public void Heal(int amount)
    {
        // Heilt den Turm und �berschreitet nicht das Maximum
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHealthBar(); // Aktualisiert den UI-Balken
        UpdateVignette();
    }

    public void TakeDamage(int damage)
    {
        // Reduziert die Lebenspunkte und pr�ft, ob der Turm zerst�rt ist
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }

        UpdateHealthBar();
        UpdateVignette(); 
    }



    void Die()
    {
        Debug.Log("Tower destroyed.");
        // Optionale Sterbeanimation oder Logik hier
        gameObject.SetActive(false); // Deaktiviert das Turm-GameObject
    }

    void UpdateHealthBar()
    {
        // Aktualisiert den Lebensbalken, falls einer zugewiesen wurde
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
    }

    void UpdateVignette()
    {
        if (vignetteEffect != null)
    {
        float healthPercentage = (float)currentHealth / maxHealth;

        if (healthPercentage > 0.5f)
        {
            // Vor der Hälfte der Lebenspunkte bleibt die Intensität bei 0
            vignetteEffect.intensity.value = 0f;
        }
        else
        {
            // Ab der Hälfte der Lebenspunkte steigt die Intensität progressiv an
            float normalizedHealth = (0.65f - healthPercentage) / 0.5f; // Wert zwischen 0 und 1
            vignetteEffect.intensity.value = Mathf.Lerp(0f, 0.3f, normalizedHealth);
        }
    }
    }
}
