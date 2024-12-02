using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    public int damage = 2;
    private TowerController towerController;
    private GameObject Tower;
    public float lastDamageTime;


    // Start is called before the first frame update
    void Start()
    {   
        Tower = GameObject.Find("Tower");
        towerController = Tower.GetComponent<TowerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, Tower.transform.position));
        if(Vector3.Distance(transform.position, Tower.transform.position) < 1.5 && Time.time - lastDamageTime > 2f)
        {
            towerController.TakeDamage(damage);
            lastDamageTime = Time.time;
        } 
    }

}
