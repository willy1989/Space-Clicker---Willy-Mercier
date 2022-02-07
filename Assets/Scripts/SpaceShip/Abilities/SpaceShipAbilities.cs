using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceShipAbilities : MonoBehaviour
{
    private bool canUseAbility = true;

    [SerializeField] private SpaceShipAbilitiesData spaceShipAbilityData;

    protected float Effect
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

    protected float Cost
    {
        get
        {
            return PlayerPrefs.GetFloat(GetCostPlayerPrefName(), spaceShipAbilityData.StartCost);
        }

        set
        {
            PlayerPrefs.SetFloat(GetCostPlayerPrefName(), value);
        }
    }

    protected abstract string GetEffectPlayerPrefName();
    protected abstract string GetCostPlayerPrefName();

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
        Effect *= spaceShipAbilityData.EffectIncreaseRate;
    }

    private void UpGradeAbility()
    {
        if(CurrencyManager.Instance.HasSufficientBalance(cost: Cost) == true)
        {
            CurrencyManager.Instance.SpendCurrency(amount: Cost);

            IncreaseAbilityEffect();
            IncreaseAbilityCost();
        }
    }
}
