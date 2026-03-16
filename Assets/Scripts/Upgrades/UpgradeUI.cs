using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] GameObject panel;

    [SerializeField] Button[] upgradeButtons;
    [SerializeField] TextMeshProUGUI[] upgradeTexts;
    [SerializeField] TextMeshProUGUI[] descriptionText;

    [SerializeField] UpgradeType[] upgradePool;

    UpgradeManager upgradeManager;
    WaveManager waveManager;

    void Start()
    {
        upgradeManager = FindObjectOfType<UpgradeManager>();
        waveManager = FindObjectOfType<WaveManager>();

        panel.SetActive(false);
    }

    public void ShowUpgrades()
    {
        panel.SetActive(true);

        List<UpgradeType> available = new List<UpgradeType>();

        foreach (var upgrade in upgradePool)
        {
            if (upgradeManager.CanOfferUpgrade(upgrade))
            {
                available.Add(upgrade);
            }
        }

        for (int i = 0; i < upgradeButtons.Length && available.Count > 0; i++)
        {
            int index = Random.Range(0, available.Count);
            UpgradeType upgrade = available[index];

            available.RemoveAt(index);

            upgradeTexts[i].text = GetUpgradeName(upgrade);
            descriptionText[i].text = GetUpgradeDescription(upgrade);

            upgradeButtons[i].onClick.RemoveAllListeners();
            upgradeButtons[i].onClick.AddListener(() =>
            {
                upgradeManager.ApplyUpgrade(upgrade);
                panel.SetActive(false);

                waveManager.StartWave();
            });
        }
    }

    string GetUpgradeName(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.RapidFire:
                return "Rapid Fire";
            case UpgradeType.PowerShot:
                return "Power Shot";
            case UpgradeType.SpreadShot:
                return "Spread Shot";
            case UpgradeType.Piercing:
                return "Piercing";
            case UpgradeType.MaxHealth:
                return "Max Health";
            case UpgradeType.Heal:
                return "Heal";
            default:
                return type.ToString();
        }
    }

    string GetUpgradeDescription(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.RapidFire:
                return "-20% Shot Cooldown";

            case UpgradeType.PowerShot:
                return "+5 Bullet Damage";

            case UpgradeType.SpreadShot:
                return "+1 Bullet Count";

            case UpgradeType.Piercing:
                return "Bullets Pierce Enemies";

            case UpgradeType.MaxHealth:
                return "+20 Max Health";

            case UpgradeType.Heal:
                return "Restore 50 Health";

            default:
                return "";
        }
    }
}