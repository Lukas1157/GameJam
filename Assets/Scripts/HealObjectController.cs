using UnityEngine;
using System.Collections;

public class HealObjectController : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;
    public LayerMask playerLayer;

    //public Transform attackPoint;
    public int healAmount = 20; // Menge an Lebenspunkten, die wiederhergestellt werden
    //public PlayerController targetPlayer; // Referenz zum Spieler oder einem anderen Objekt, dessen Leben wiederhergestellt werden soll

    private Animator animator;
    //private float lastAttackTime = 0;
    private ParticleSystem BoxExplosionParticle;
    private ParticleSystem destroyParticle;
    private ParticleSystem floatingParticle;
    private bool isDestroyed = false;

    public float respawnTime = 10f;

 



    void OnTriggerEnter2D(Collider2D collision){

    if (collision.CompareTag("Sword") && isDestroyed == false){
         BoxExplosionParticle.Play(false);
            TakeDamage(10);
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

        destroyParticle = transform.GetChild(0).GetComponent<ParticleSystem>();
        destroyParticle.Stop();
		floatingParticle = transform.GetChild(1).GetComponent<ParticleSystem>();
		floatingParticle.Play();

    }

       

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
     
        if (currentHealth <= 0)
        {
            DestroyObject();
        }
    }

    void DestroyObject()
    {
        

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

       isDestroyed = true;
        destroyParticle.Play();
        floatingParticle.Stop();
        transform.GetComponent<Renderer>().enabled = false;
         transform.GetComponent<Collider2D>().enabled = false;
        StartCoroutine(DestroyScenario());
              
    }

    IEnumerator DestroyScenario()
    {
        while (destroyParticle.isPlaying)
        {
            yield return null;
        } 
        
         yield return new WaitForSeconds(respawnTime);
         currentHealth = maxHealth;
        isDestroyed = false;
        transform.GetComponent<Renderer>().enabled = true;
        transform.GetComponent<Collider2D>().enabled = true;
        floatingParticle.Play();
        destroyParticle.Play();
        Debug.Log("Kiste ist respawned!");
    }

   
}
