﻿using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private int returnScene = -1;
    private LevelController lc;
    private PlayerController pc;

    void Awake()
    {
        lc = GetComponent<LevelController>();
        pc = GetComponent<PlayerController>();
    }

    public void ChangeLevel(int index)
    {
        print("change level");
        if (pc)
        {
            // bonuses here!
            print("change level passed");
            if (!PlayerPrefs.HasKey("Level_" + lc.levelNumber + "_score"))
            {
                PlayerPrefs.SetInt("Level_" + lc.levelNumber + "_score", 0);
            }
            if (pc.score > PlayerPrefs.GetInt("Level_" + lc.levelNumber + "_score"))
            {
                print("[Level_" + lc.levelNumber + "_score] is set to " + pc.score.ToString());
                PlayerPrefs.SetInt("Level_" + lc.levelNumber + "_score", pc.score);
            }
        }

        if (index == -1)
        {
            Application.Quit();
        }
        else
        {
            Application.LoadLevel(index);
        }
    }

    void Update()
    {
        if (!pc)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (returnScene == -1)
                {
                    Application.Quit();
                }
                else
                {
                    ChangeLevel(returnScene);
                }
            }
        }
    }
}
