using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerStats : MonoBehaviour
{
    public int currentHealth = 0;
    public int maxHealth = 100;

    [SerializeField] private int expPoints = 0;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public float GetHealthPercentage()
    {
        return (currentHealth / (float)maxHealth * 100);
    }

    private void PlayerDied()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);

    }

    private void Update()
    {

        if (currentHealth <= 0)
        {
            this.PlayerDied();
        }

    }

    public void addExperience(int expPoints)
    {
        this.expPoints += expPoints;
    }
    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}
