using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;
    public int attackDamage = 10;
    public float attackCooldown = 2.0f;
    public float attackRange = 1.0f;
    public LayerMask playerLayer;

    public Transform attackPoint;
    public int healAmount = 20; // Menge an Lebenspunkten, die wiederhergestellt werden
    public PlayerController targetPlayer; // Referenz zum Spieler oder einem anderen Objekt, dessen Leben wiederhergestellt werden soll

    private Animator animator;
    private float lastAttackTime = 0;
    ParticleSystem BoxExplosionParticle;



    void OnTriggerEnter2D(Collider2D collision){

    if (collision.CompareTag("Sword")){
         BoxExplosionParticle.Play();
    } else{
        BoxExplosionParticle.Stop();
    }
   

    }

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        BoxExplosionParticle=GetComponent<ParticleSystem>();
        BoxExplosionParticle.Stop();
    }

    void Update()
    {
        // Gegner greift automatisch an, wenn der Spieler in Reichweite ist
        Collider2D player = Physics2D.OverlapBox(attackPoint.position, new Vector2(attackRange, attackRange), 0, playerLayer);

        if (player != null && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack(player.GetComponent<PlayerController>());
            lastAttackTime = Time.time;
        }
    }

    void Attack(PlayerController player)
    {
        // Spielt die Angriff-Animation ab
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // Fügt dem Spieler Schaden zu
        player.TakeDamage(attackDamage);
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
        Debug.Log("Enemy died.");
        // Spielt die Sterbeanimation ab
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        // Heilung eines Objekts mit dem Layer "Tower"
        GameObject tower = FindObjectOfType<TowerController>()?.gameObject; // Findet ein Tower-Objekt
        if (tower != null && tower.layer == LayerMask.NameToLayer("Tower"))
        {
            TowerController towerController = tower.GetComponent<TowerController>();
            if (towerController != null)
            {
                towerController.Heal(healAmount); // Heilt den Turm
            }
        }

        // Gegner deaktivieren
        gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        // Zeichnet den Angriffsradius im Editor
        if (attackPoint == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(attackPoint.position, new Vector3(attackRange, attackRange, 0));
    }
}
