using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearStage_Script : MonoBehaviour
{
    public GameObject dropSword;
    public GameObject potal;

    public static bool getSword;
    public static bool killMiddleBoss;
    public static int killEnemyCount;
    public int goalKillCount;

    private string nowStage;
    private bool spawnSword;

    private void Start()
    {
        spawnSword = false;
        getSword = false;
        killMiddleBoss = false;
        killEnemyCount = 0;

        nowStage = PlayerPrefs.GetString("PlayStage");
    }

    private void Update()
    {
        if (nowStage == "Stage_01")
        {
            if (!spawnSword && killEnemyCount == goalKillCount)
            {
                spawnSword = true;
                dropSword.SetActive(true);
            }
        }
        else if (nowStage == "Stage_02")
        {
            if (!spawnSword && killEnemyCount == goalKillCount)
            {
                spawnSword = true;
                dropSword.SetActive(true);
            }
        }
        else
        {
            if (!spawnSword && killMiddleBoss)
            {
                spawnSword = true;
                potal.SetActive(true);
            }
        }

        if (getSword)
        {
            potal.SetActive(true);
        }
    }
}
