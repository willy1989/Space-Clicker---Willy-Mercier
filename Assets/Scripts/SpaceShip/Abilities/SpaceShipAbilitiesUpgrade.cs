using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipAbilitiesUpgrade : MonoBehaviour
{
    [SerializeField] private SpaceShipAbilities ability;

    [SerializeField] private Button upgradeButton;

    [SerializeField] private Text costText;

    private void Awake()
    {
        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(ability.UpGradeAbility);
    }

    private void Start()
    {
        ability.UpgradeAbilityEvent += UpdateCostText;

        UpdateCostText();
    }

    private void UpdateCostText()
    {
        costText.text = Utils.ConvertNumberToShortText(ability.Cost);
    }


}
