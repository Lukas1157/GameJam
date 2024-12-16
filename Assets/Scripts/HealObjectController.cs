using UnityEngine;
using System.Collections;

public class HealObjectController : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;
    public LayerMask playerLayer;

    public Transform attackPoint;
    public int healAmount = 20; // Menge an Lebenspunkten, die wiederhergestellt werden
    public PlayerController targetPlayer; // Referenz zum Spieler oder einem anderen Objekt, dessen Leben wiederhergestellt werden soll

    private Animator animator;
    private float lastAttackTime = 0;
    private ParticleSystem BoxExplosionParticle;
    private ParticleSystem destroyParticle;

 



    void OnTriggerEnter2D(Collider2D collision){

    if (collision.CompareTag("Sword")){
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

    }

    void Update()
    {
        
    }

    

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
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

        Debug.Log("zerstört die kiste");
        destroyParticle.Play();
        transform.GetComponent<Renderer>().enabled = false;
        StartCoroutine(DestroyScenario());
              
    }

    IEnumerator DestroyScenario()
    {
        while (destroyParticle.isEmitting)
        {
            yield return null;
        } 
        DestroyObject(gameObject);
    }

   
}
