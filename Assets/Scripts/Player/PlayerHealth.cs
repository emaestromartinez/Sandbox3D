using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth = 0;
    public int maxHealth = 100;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        InvokeRepeating("LowerHealth", 1.0f, 1.0f);
    }

    public float GetHealthPercentage()
    {
        return (currentHealth / (float)maxHealth * 100);
    }
    private void LowerHealth()
    {
        if (currentHealth < 0)
        {
            currentHealth = 100;
            healthBar.SetHealth(currentHealth);
        }
        else
        {
            currentHealth -= 12;

            healthBar.SetHealth(currentHealth);
        }

    }

    private void Update()
    {

        // DamagePlayer(10);

    }

    public void DamagePlayer(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}
