using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    public int damage = 2;
    public HealthTower towerHealth;
    public GameObject Tower;
    public float lastDamageTime;


    // Start is called before the first frame update
    void Start()
    {   
        Tower = GameObject.Find("Tower");
        towerHealth = Tower.GetComponent<HealthTower>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Vector3.Distance(transform.position, Tower.transform.position) < 1.5 && Time.time - lastDamageTime > 2f)
        {
            towerHealth.TakeDamage(damage);
            lastDamageTime = Time.time;
        } 
    }

}
