using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerStats : MonoBehaviour
{
    public HealthBar healthBar;

    public int maxHealth = 100;
    [SerializeField] private int expPoints = 0;
    [SerializeField] private CharacterStat weaponPower = new CharacterStat(10);
    [SerializeField] private CharacterStat armor = new CharacterStat(1);
    private static int kills;

    [SerializeField] private int currentHealth = 0;
    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            if (value < 0) currentHealth = 0;
            else currentHealth = value;
        }
    }




    private void Awake()
    {

    }
    void Start()
    {
        maxHealth = 100 + Mathf.RoundToInt(weaponPower.Value); ;
        CurrentHealth = maxHealth;
        this.expPoints = GameMaster.GM.levelSystem.GetExperience();

        GameMaster.GM.levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        GameMaster.GM.levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        // On gettin lvl 2 we will end the run;
        // Scene thisScene = SceneManager.GetSceneByName("MainMenuScene");
        // SceneManager.LoadScene(thisScene.name);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
    }

    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        this.expPoints = GameMaster.GM.levelSystem.GetExperience(); // Only to show it in the unity inspector;
    }

    public float GetHealthPercentage()
    {
        return (CurrentHealth / (float)maxHealth * 100);
    }

    private void PlayerDied()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

    private void Update()
    {
        if (CurrentHealth <= 0)
        {
            this.PlayerDied();
        }
    }

    public void addExperience(int expPoints)
    {
        GameMaster.GM.levelSystem.AddExperience(expPoints);
        this.expPoints = GameMaster.GM.levelSystem.GetExperience();
    }
    public void TakeDamage(int damage)
    {
        damage = damage - Mathf.RoundToInt(armor.Value);
        if (damage <= 0) return;
        CurrentHealth -= damage;

        healthBar.SetHealth(CurrentHealth);
    }
}
