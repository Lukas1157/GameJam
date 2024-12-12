using UnityEngine;
using UnityEngine.UI;

public class TowerController : MonoBehaviour
{
    public int maxHealth = 10; // Maximale Lebenspunkte des Turms
    public int currentHealth; // Aktuelle Lebenspunkte
    public TowerHealthbar healthBar; // UI-Slider f�r die Lebenspunkte (falls ben�tigt)

    void Start()
    {
        currentHealth = maxHealth; // Setzt die Lebenspunkte auf das Maximum
        healthBar.SetMaxHealth(maxHealth); // Initialisiert den Lebensbalken
    }

    public void Heal(int amount)
    {
        // Heilt den Turm und �berschreitet nicht das Maximum
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHealthBar(); // Aktualisiert den UI-Balken
    }

    public void TakeDamage(int damage)
    {Debug.Log(damage);
        // Reduziert die Lebenspunkte und pr�ft, ob der Turm zerst�rt ist
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }

        UpdateHealthBar();
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
}
