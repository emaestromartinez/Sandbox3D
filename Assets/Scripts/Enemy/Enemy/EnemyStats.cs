using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    // Player
    protected GameObject player;

    // Enemy stats
    public float currentHealth;
    public float maxHealth;
    protected int expPoints;

    public GameObject enemyGenerator;

    public GameObject healthBarUI;
    public Slider slider;

    public void TakeDamage(int damage)
    {
        if (damage < 0) return;
        if (damage > currentHealth) currentHealth = 0;
        else currentHealth -= damage;
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
    protected float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }
}
