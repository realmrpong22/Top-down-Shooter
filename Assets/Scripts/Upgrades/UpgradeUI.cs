using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] GameObject panel;

    [SerializeField] Button[] upgradeButtons;
    [SerializeField] TextMeshProUGUI[] upgradeTexts;

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

        List<UpgradeType> available = new List<UpgradeType>(upgradePool);

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            int index = Random.Range(0, available.Count);
            UpgradeType upgrade = available[index];

            available.RemoveAt(index);

            upgradeTexts[i].text = upgrade.ToString();

            upgradeButtons[i].onClick.RemoveAllListeners();
            upgradeButtons[i].onClick.AddListener(() =>
            {
                upgradeManager.ApplyUpgrade(upgrade);
                panel.SetActive(false);

                waveManager.StartWave();
            });
        }
    }
}