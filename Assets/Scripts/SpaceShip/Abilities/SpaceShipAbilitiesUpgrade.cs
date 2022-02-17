using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipAbilitiesUpgrade : MonoBehaviour
{
    [SerializeField] private SpaceShipAbilities ability;

    [SerializeField] private Button upgradeButton;

    [SerializeField] private Text costText;

    private Color purchaseAvailableColor = new Color(0.9f, 0.6f, 0f);

    private Color originalColor = Color.white;

    private void Awake()
    {
        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(ability.UpGradeAbility);
    }

    private void Start()
    {
        ability.UpgradeAbilityEvent += UpdateCostText;

        HighlightButtons();

        CurrencyManager.Instance.UpdateCurrencyEvent += HighlightButtons;

        UpdateCostText();
    }

    private void UpdateCostText()
    {
        costText.text = Utils.AbreviateNumber(ability.Cost);
    }

    private void HighlightButtons()
    {
        if (CurrencyManager.Instance.CurrencyCount >= ability.Cost)
            upgradeButton.image.color = purchaseAvailableColor;
        else
            upgradeButton.image.color = originalColor;
    }


}
