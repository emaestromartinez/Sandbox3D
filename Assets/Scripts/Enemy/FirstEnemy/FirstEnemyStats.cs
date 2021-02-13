using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstEnemyStats : EnemyStats
{


    private void Start()
    {
        // Set the stats for that particular enemy.
        this.expPoints = 33;

        player = GameObject.FindGameObjectsWithTag("Player")[0];
        currentHealth = maxHealth;
        slider.value = CalculateHealth();

        enemyGenerator = GameObject.Find("EnemyGenerator");
    }

    protected void Update()
    {
        slider.value = CalculateHealth();
        if (currentHealth <= 0)
        {
            if (enemyGenerator)
            {
                GameMaster.GM.levelSystem.AddExperience(expPoints);
                enemyGenerator.GetComponent<EnemyGenerator>().EnemyDied();
            }
            Destroy(gameObject);
        }
        else
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        if (currentHealth < maxHealth)
        {
            healthBarUI.SetActive(true);
        }
    }
}
