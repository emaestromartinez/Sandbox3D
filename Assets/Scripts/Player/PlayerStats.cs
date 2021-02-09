using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerStats : MonoBehaviour
{



    public HealthBar healthBar;

    public int currentHealth = 0;
    public int maxHealth = 100;
    [SerializeField] private int expPoints = 0;
    [SerializeField] private CharacterStat armor = new CharacterStat(1);
    [SerializeField] private CharacterStat strength = new CharacterStat(10);

    private LevelSystem levelSystem;

    private void Awake()
    {

    }
    void Start()
    {
        maxHealth = 100 + Mathf.RoundToInt(strength.Value); ;
        currentHealth = maxHealth;
    }
    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        // levelSystem.onLevelChanged
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
        damage = damage - Mathf.RoundToInt(armor.Value);
        if (damage <= 0) return;
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}
