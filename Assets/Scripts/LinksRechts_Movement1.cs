using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinksRechts_Movement : MonoBehaviour
{
    public List<Transform> points;
    int goalPoint = 0;
    public float moveSpeed = 2;
    public Transform platform;

 

    private void Update(){
         
            MoveToNextPoint();
 
    }



    void MoveToNextPoint(){

    platform.position= Vector2.MoveTowards(platform.position, points[goalPoint].position, 1*Time.deltaTime*moveSpeed);

    if(Vector2.Distance(platform.position, points[goalPoint].position)<0.1f){
       
    if(goalPoint==points.Count-1){
        goalPoint = 0;
    } else {
        goalPoint++;
    }
    }

    }
    
}
