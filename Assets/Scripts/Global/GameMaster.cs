using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster GM;

    public GameObject soundManager;
    public LevelSystem levelSystem;

    private void Awake()
    {
        if (GM != null)
            GameObject.Destroy(GM);
        else
            GM = this;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        levelSystem = new LevelSystem();
    }
}


