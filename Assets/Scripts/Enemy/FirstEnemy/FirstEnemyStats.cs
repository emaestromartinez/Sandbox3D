using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstEnemyStats : EnemyStats
{

    private void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];

        currentHealth = maxHealth;
        slider.value = CalculateHealth();

        enemyGenerator = GameObject.Find("EnemyGenerator");
    }
}
