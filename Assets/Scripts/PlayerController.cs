using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Angriff wird jetzt vollständig im HeroKnight-Skript verarbeitet
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attack action is now handled by HeroKnight script.");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died.");
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }
        gameObject.SetActive(false);
    }
}
