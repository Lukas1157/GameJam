using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float speed;
    public float input;
    
    void Start()
    {
        input = 0;
    }
    // Update is called once per frame


    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
    }
}
