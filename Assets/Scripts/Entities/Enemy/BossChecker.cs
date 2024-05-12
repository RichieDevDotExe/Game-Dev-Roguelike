using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheker : MonoBehaviour
{
    private Enemy boss;
    private Canvas playerUI;
    private Canvas nextLevelUI;

    void Start()
    {
        boss = GetComponent<Enemy>();
        playerUI = GameObject.Find("PlayerUI").GetComponent<Canvas>();
        nextLevelUI = GameObject.Find("NextLevelUI").GetComponent<Canvas>();
    }

    private void Update()
    {
        if (boss.EntityHealth <= 0)
        {
            Time.timeScale = 0f;
            playerUI.enabled = false;
            nextLevelUI.enabled = true;
        }
    }
}
