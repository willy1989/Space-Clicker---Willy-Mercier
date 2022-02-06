using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonUpgradeUI : MonoBehaviour
{
    [SerializeField] private CannonUpgradeData cannonUpgradeData;

    [Header("Damage")]

    [SerializeField] private Button upgradeDamageButton;

    [SerializeField] private Text upgradeDamageCost;

    
    [Header("Frequency")]

    [SerializeField] private Button upgradeFrequencyButton;

    [SerializeField] private Text upgradeFrequencyCost;



    private void Awake()
    {
        upgradeDamageButton.onClick.RemoveAllListeners();

        upgradeDamageButton.onClick.AddListener(cannonUpgradeData.UpGradeDamage);

        upgradeDamageButton.onClick.AddListener(UpdateDamageCostText);



        upgradeFrequencyButton.onClick.RemoveAllListeners();

        upgradeFrequencyButton.onClick.AddListener(cannonUpgradeData.UpGradeFrequency);

        upgradeFrequencyButton.onClick.AddListener(UpdateFrequencyCostText);
    }

    private void Start()
    {
        UpdateDamageCostText();
        UpdateFrequencyCostText();
    }


    private void UpdateDamageCostText()
    {
        upgradeDamageCost.text = Utils.ConvertNumberToShortText(cannonUpgradeData.NextDamageUpGradeCost);
    }

    private void UpdateFrequencyCostText()
    {
        upgradeFrequencyCost.text = Utils.ConvertNumberToShortText(cannonUpgradeData.NextFrequencyUpGradeCost);
    }
}
