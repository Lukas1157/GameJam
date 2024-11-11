using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealthbar : MonoBehaviour
{
    public Slider Towerslider;


    public void SetMaxHealth(int health)
    {
        Towerslider.maxValue = health;
        Towerslider.value = health;
    }


    public void SetHealth(int health)
    {
      Towerslider.value = health;
    }
}
