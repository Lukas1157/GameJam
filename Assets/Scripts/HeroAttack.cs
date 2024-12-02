using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    private BoxCollider2D SwordCollider;

    //SwordCollider
    private IEnumerator HitAttack()
    {
        SwordCollider.enabled = true;

        // Finden von Gegnern im Bereich des Schwertcolliders
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(SwordCollider.bounds.center, SwordCollider.bounds.size, 0);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                EnemyController enemyController = enemy.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    enemyController.TakeDamage(25); // Beispielwert für Schaden
                }
            }
        }

        yield return new WaitForSeconds(0.2f);
        SwordCollider.enabled = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
