using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    PlayerStats playerStats;
    Health playerHealth;

    int spreadShotCount = 0;
    int pierceCount = 0;
    int rapidFireCount = 0;

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

            case UpgradeType.SpreadShot:
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

    public bool CanOfferUpgrade(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.RapidFire:
                return rapidFireCount < 3;
            case UpgradeType.SpreadShot:
                return spreadShotCount < 2;
            case UpgradeType.Piercing:
                return pierceCount < 1;
            case UpgradeType.Heal:
                return playerHealth.healthPercentage < 1;
            default:
                return false;
        }
    }
}