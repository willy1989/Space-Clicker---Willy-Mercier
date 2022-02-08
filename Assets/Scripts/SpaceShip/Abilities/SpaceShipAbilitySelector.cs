using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipAbilitySelector : MonoBehaviour
{
    [SerializeField] private SpaceShipAbilities ability;

    [SerializeField] private Button SelectAbilityButton;

    private void Awake()
    {
        SelectAbilityButton.onClick.AddListener(SelectAbility);
    }

    private void SelectAbility()
    {
        SpaceShipAbilitiesManager.Instance.SetCurrentAbility(ability: ability);
    }
}
