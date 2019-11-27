using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
    public Image HealthBar;
    public static float PlayerHealth = 100;
    private void Update()
    {
        CheckHealth();
        UpdateHealthBar();

    }

    private void UpdateHealthBar()
    {
        HealthBar.fillAmount = PlayerHealth / 100;
    }

    private void CheckHealth()
    {
        if (PlayerHealth <= 0)
        {
            Debug.Log("GameOver");
            return;
        }
        if (PlayerHealth < 100)
        {
            Debug.Log(PlayerHealth);
            PlayerHealth += .20f;
        }
        else
            PlayerHealth = 100;


    }
}
