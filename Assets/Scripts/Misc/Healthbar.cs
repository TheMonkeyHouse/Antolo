using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;

    public void InitHealthBar(float maxHP){
        this.healthBar.maxValue = maxHP;
        this.healthBar.value = maxHP;
    }

    public void UpdateCurrentHealth(float hp)
    {
        this.healthBar.value =  hp;
    }
}