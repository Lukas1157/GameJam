using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealthbar : MonoBehaviour
{
    public Slider towerSlider;
    private void Start()
    {
        towerSlider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int health)
    {
        towerSlider.maxValue = health;
        towerSlider.value = health;
    }


    public void SetHealth(int health)
    {
      towerSlider.value = health;
    }
}
