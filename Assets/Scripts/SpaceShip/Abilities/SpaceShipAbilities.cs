using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceShipAbilities : MonoBehaviour
{
    [SerializeField] private SpaceShipAbilitiesData spaceShipAbilityData;

    private bool canUseAbility = true;

    protected float effect
    {
        get
        {
            return PlayerPrefs.GetFloat(GetEffectPlayerPrefName(), spaceShipAbilityData.StartEffect);
        }

        set
        {
            PlayerPrefs.SetFloat(GetEffectPlayerPrefName(), value);
        }
    }

    public float Cost
    {
        get
        {
            return PlayerPrefs.GetFloat(GetCostPlayerPrefName(), spaceShipAbilityData.StartCost);
        }

        private set
        {
            PlayerPrefs.SetFloat(GetCostPlayerPrefName(), value);
            UpgradeAbilityEvent.Invoke();
        }
    }

    protected abstract string GetEffectPlayerPrefName();
    protected abstract string GetCostPlayerPrefName();

    public Action UpgradeAbilityEvent;

    private void Start()
    {
        PlayerPrefs.SetFloat(GetEffectPlayerPrefName(), spaceShipAbilityData.StartEffect);
        PlayerPrefs.SetFloat(GetCostPlayerPrefName(), spaceShipAbilityData.StartCost);
    }

    public void UseAbility()
    {
        if (canUseAbility == false)
            return;

        DoAbility();

        StartCoroutine(startCoolDownCoroutine());
    }

    private IEnumerator startCoolDownCoroutine()
    {
        canUseAbility = false;

        yield return new WaitForSeconds(spaceShipAbilityData.CoolDownDuration);

        canUseAbility = true;
    }

    protected abstract void DoAbility();

    private void IncreaseAbilityCost()
    {
        Cost *= spaceShipAbilityData.CostIncreaseRate;
    }

    private void IncreaseAbilityEffect()
    {
        effect *= spaceShipAbilityData.EffectIncreaseRate;
    }

    public void UpGradeAbility()
    {
        if(CurrencyManager.Instance.HasSufficientBalance(cost: Cost) == true)
        {
            CurrencyManager.Instance.SpendCurrency(amount: Cost);

            IncreaseAbilityEffect();
            IncreaseAbilityCost();
        }
    }
}
