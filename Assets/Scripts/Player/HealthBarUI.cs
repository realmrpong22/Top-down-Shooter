using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image healthBarBackground;
    public Image healthBarFill;
    public Transform player;
    Health health;

    void Start()
    {
        health = player.GetComponent<Health>();
        UpdateHealthBar();
    }

    void Update()
    {
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthBarFill.fillAmount = health.healthPercentage;
    }
}
