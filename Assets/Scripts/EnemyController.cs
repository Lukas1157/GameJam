using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;
    //public int attackDamage = 10;
    //public float attackCooldown = 2.0f;
    //public float attackRange = 1.0f;
    public LayerMask playerLayer;

    public Transform attackPoint;
      public PlayerController targetPlayer; // Referenz zum Spieler oder einem anderen Objekt, dessen Leben wiederhergestellt werden soll

    private Animator animator;
    //private float lastAttackTime = 0;
   private ParticleSystem hitParticle;
    private ParticleSystem destroyParticle;
    private bool isDead = false;



    void OnTriggerEnter2D(Collider2D collision){

    if (collision.CompareTag("Sword") && isDead == false){
         hitParticle.Play(false);
                
    } else{
        hitParticle.Stop();
    }
   

    }

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        hitParticle= transform.GetChild(0).GetComponent<ParticleSystem>();
               destroyParticle = transform.GetChild(1).GetComponent<ParticleSystem>();
      

    }

    void Update()
    {
        // Gegner greift automatisch an, wenn der Spieler in Reichweite ist
       /* Collider2D player = Physics2D.OverlapBox(attackPoint.position, new Vector2(attackRange, attackRange), 0, playerLayer);

        if (player != null && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack(player.GetComponent<PlayerController>());
            lastAttackTime = Time.time;
        } */
    }

    /*void Attack(PlayerController player)
    {
        // Spielt die Angriff-Animation ab
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // Fügt dem Spieler Schaden zu
        player.TakeDamage(attackDamage);
    } */

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
       
        isDead =  true;

        destroyParticle.Play();
        //transform.GetComponent<Renderer>().enabled = false;
        StartCoroutine(DieScenario());

    }

    IEnumerator DieScenario()
    {
       yield return new WaitForSeconds(destroyParticle.main.duration);

             Destroy(gameObject);
    }

    
}
