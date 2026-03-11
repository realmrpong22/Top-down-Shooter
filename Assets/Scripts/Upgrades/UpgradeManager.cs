using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    PlayerStats playerStats;
    Health playerHealth;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    public void ApplyUpgrade(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.RapidFire:
                playerStats.shotDelay *= 0.8f;
                break;

            case UpgradeType.PowerShot:
                playerStats.bulletDamage += 5f;
                break;

            case UpgradeType.DoubleShot:
                playerStats.bulletCount += 1;
                break;

            case UpgradeType.Piercing:
                playerStats.pierceCount += 1;
                break;

            case UpgradeType.MaxHealth:
                playerHealth.AddMaxHealth(20);
                break;

            case UpgradeType.Heal:
                playerHealth.AddHealth(30);
                break;
        }
    }
}