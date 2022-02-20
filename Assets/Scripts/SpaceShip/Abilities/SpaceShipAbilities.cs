using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceShipAbilities : MonoBehaviour
{
    [SerializeField] protected SpaceShipAbilitiesData spaceShipAbilityData;

    protected JsonDataUser<SpaceShipAbilityPersistentData> jsonDataUser;

    protected string jsonFileName;

    public float Cost
    {
        get
        {
            return jsonDataUser.JsonData.Cost;
        }
    }

    public float Effect
    {
        get
        {
            return jsonDataUser.JsonData.Effect;
        }
    }

    private bool canUseAbility = true;

    public Action UpgradeAbilityEvent;

    public Action<float> StartAbilityCoolDownEvent;

    protected abstract void SetJasonFileName();

    public void UseAbility()
    {
        if (canUseAbility == false)
            return;

        DoAbility();

        StartCoroutine(StartCoolDownCoroutine());
    }

    private IEnumerator StartCoolDownCoroutine()
    {
        canUseAbility = false;

        StartAbilityCoolDownEvent(spaceShipAbilityData.CoolDownDuration);

        yield return new WaitForSeconds(spaceShipAbilityData.CoolDownDuration);

        canUseAbility = true;
    }

    protected abstract void DoAbility();

    private void IncreaseAbilityCost()
    {
        jsonDataUser.JsonData.Cost *= spaceShipAbilityData.CostIncreaseRate;
    }

    private void IncreaseAbilityEffect()
    {
        jsonDataUser.JsonData.Effect *= spaceShipAbilityData.EffectIncreaseRate;
    }

    public void UpGradeAbility()
    {
        if(CurrencyManager.Instance.HasSufficientBalance(cost: jsonDataUser.JsonData.Cost) == true)
        {
            CurrencyManager.Instance.SpendCurrency(amount: jsonDataUser.JsonData.Cost);

            IncreaseAbilityEffect();
            IncreaseAbilityCost();

            UpgradeAbilityEvent.Invoke();
        }
    }
}
