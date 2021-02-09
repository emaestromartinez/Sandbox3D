/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Awake()
    {
        LevelSystem levelSystem = new LevelSystem();

        LevelSystemAnimated levelSystemAnimated = new LevelSystemAnimated(levelSystem);
        player.SetLevelSystemAnimated(levelSystemAnimated);
    }

}
