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

    private Color purchaseAvailableColor = new Color(0.9f, 0.6f, 0f);

    private Color originalColor = Color.white;



    private void Awake()
    {
        upgradeDamageButton.onClick.AddListener(cannonUpgradeData.UpGradeDamage);

        upgradeDamageButton.onClick.AddListener(UpdateDamageCostText);


        upgradeFrequencyButton.onClick.AddListener(cannonUpgradeData.UpGradeFrequency);

        upgradeFrequencyButton.onClick.AddListener(UpdateFrequencyCostText);
    }

    private void Start()
    {
        UpdateDamageCostText();
        UpdateFrequencyCostText();

        CurrencyManager.Instance.UpdateCurrencyEvent += HighlightButtons;
    }

    private void UpdateDamageCostText()
    {
        upgradeDamageCost.text = Utils.AbreviateNumber(cannonUpgradeData.NextDamageUpGradeCost);
    }

    private void UpdateFrequencyCostText()
    {
        upgradeFrequencyCost.text = Utils.AbreviateNumber(cannonUpgradeData.NextFrequencyUpGradeCost);
    }

    private void HighlightButtons(float currencyCount)
    {
        if (currencyCount >= cannonUpgradeData.NextDamageUpGradeCost)
            upgradeDamageButton.image.color = purchaseAvailableColor;
        else
            upgradeDamageButton.image.color = originalColor;

        if (currencyCount >= cannonUpgradeData.NextFrequencyUpGradeCost)
            upgradeFrequencyButton.image.color = purchaseAvailableColor;
        else
            upgradeFrequencyButton.image.color = originalColor;
    }


}
