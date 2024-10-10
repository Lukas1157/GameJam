using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private int Health;
    private PolygonCollider2D Collider;

    // Start is called before the first frame update
    void Start()
    {
        Health = 5;
        Collider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health == 0)
        { 
        // Hier was passiert bei Health Null???
        }
        
        }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Sword")
        {
            Health = Health - 1;
        }
    }
}

