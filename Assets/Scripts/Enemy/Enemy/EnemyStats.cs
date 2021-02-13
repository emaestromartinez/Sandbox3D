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


    protected float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }
}
