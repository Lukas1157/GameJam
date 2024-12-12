using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTower : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;

    public TowerHealthbar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

 
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}
