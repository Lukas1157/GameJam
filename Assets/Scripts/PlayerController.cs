using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int attackDamage = 20; // Schaden des Angriffs
    public float attackRange = 1.0f; // Reichweite des Angriffs
    public LayerMask enemyLayer; // Layer für den Gegner

    public Transform attackPoint; // Punkt, von dem der Angriff ausgeht
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Spieler Angriff (Linke Maustaste)
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
       
        // Findet alle Gegner im Angriffsradius
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(attackRange, attackRange), 0, enemyLayer);

        // Fügt Gegnern Schaden zu
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<EnemyController>() != null)
            {
                enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
            }
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
        // Spielt die Sterbeanimation ab
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }
        // Spieler deaktivieren
        gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        // Zeichnet den Angriffsradius im Editor
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, new Vector3(attackRange, attackRange, 0));
    }
}
