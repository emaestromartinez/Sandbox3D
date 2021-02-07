using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public PlayerStats playerHealth;
    public Image healthBarImage;

    private float healthPercentage;

    private void Start()
    {


        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.maxHealth;
        healthPercentage = playerHealth.GetHealthPercentage();
    }

    public void SetHealth(int hp)
    {
        //Set the slider value to the amount of HP%
        healthPercentage = playerHealth.GetHealthPercentage();
        healthBar.value = healthPercentage;


        if (healthPercentage > 50f) healthBarImage.color = Color.green;
        else if (healthPercentage >= 22f)
            healthBarImage.color = Color.yellow;
        else healthBarImage.color = Color.red;
    }
}