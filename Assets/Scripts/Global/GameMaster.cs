using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster GM;

    public int totalKills;
    public GameObject soundManager;
    public LevelSystem levelSystem;

    private GUIStyle guiStyle = new GUIStyle();

    private void Awake()
    {
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else if (GM != this)
        {
            Destroy(gameObject);
        }
        //     if (GM != null)
        //         GameObject.Destroy(GM);
        //     else
        //     {
        //         GM = this;
        //         DontDestroyOnLoad(this);
        //     }

    }
    private void OnGUI()
    {
        guiStyle.fontSize = 44;
        guiStyle.normal.textColor = Color.white;

        GUI.Label(new Rect(10, 10, 200, 60), "Total kills: " + totalKills, guiStyle);
        GUI.Label(new Rect(10, 70, 200, 60), "Level: " + GameMaster.GM.levelSystem.GetLevelNumber(), guiStyle);
        GUI.Label(new Rect(10, 110, 200, 60), "Experience: " + GameMaster.GM.levelSystem.GetExperience(), guiStyle);
    }
    private void Start()
    {
        levelSystem = new LevelSystem();
    }
}


