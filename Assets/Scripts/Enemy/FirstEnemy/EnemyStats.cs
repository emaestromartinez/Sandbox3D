using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    // Player
    private GameObject player;

    // Enemy stats
    public float currentHealth;
    public float maxHealth;
    private int expPoints = 2;

    public GameObject enemyGenerator;

    public GameObject healthBarUI;
    public Slider slider;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];

        currentHealth = maxHealth;
        slider.value = CalculateHealth();

        enemyGenerator = GameObject.Find("EnemyGenerator");

        // For debugging enemy health bar UI;
        // InvokeRepeating("LowerHealth", 1.0f, 1.0f);
    }
    public void TakeDamage(int damage)
    {
        if (damage < 0) return;
        if (damage > currentHealth) currentHealth = 0;
        else currentHealth -= damage;
    }
    private void LowerHealth()
    { //function to invokeRepeat for debugging;
        if (currentHealth < 0)
        {
            currentHealth = 100f;
        }
        else
        {
            currentHealth -= 20f;
        }
    }
    void Update()
    {
        slider.value = CalculateHealth();
        if (currentHealth <= 0)
        {
            if (enemyGenerator)
            {
                player.GetComponent<PlayerStats>().addExperience(expPoints);
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
    float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }
}
