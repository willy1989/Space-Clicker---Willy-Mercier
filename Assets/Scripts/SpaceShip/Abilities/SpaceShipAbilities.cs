using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceShipAbilities : MonoBehaviour
{
    [SerializeField] protected SpaceShipAbilitiesData spaceShipAbilityData;

    protected SpaceShipAbilityPersistentData spaceShipAbilityPersistentData;

    protected string jsonFileName;

    public float Cost
    {
        get
        {
            return spaceShipAbilityPersistentData.Cost;
        }
    }

    public float Effect
    {
        get
        {
            return spaceShipAbilityPersistentData.Effect;
        }
    }

    private bool canUseAbility = true;

    public Action UpgradeAbilityEvent;

    public Action<float> StartAbilityCoolDownEvent;

    protected abstract void SetJasonFileName();

    protected void LoadData()
    {
        if (JsonDataManagement.FileExists(fileName: jsonFileName) == true)
        {
            spaceShipAbilityPersistentData = JsonDataManagement.LoadData<SpaceShipAbilityPersistentData>(fileName: jsonFileName);
        }

        else
        {
            spaceShipAbilityPersistentData = new SpaceShipAbilityPersistentData(_cost: spaceShipAbilityData.StartCost, _effect: spaceShipAbilityData.StartEffect);
        }
    }

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
        spaceShipAbilityPersistentData.Cost *= spaceShipAbilityData.CostIncreaseRate;
    }

    private void IncreaseAbilityEffect()
    {
        spaceShipAbilityPersistentData.Effect *= spaceShipAbilityData.EffectIncreaseRate;
    }

    public void UpGradeAbility()
    {
        if(CurrencyManager.Instance.HasSufficientBalance(cost: spaceShipAbilityPersistentData.Cost) == true)
        {
            CurrencyManager.Instance.SpendCurrency(amount: spaceShipAbilityPersistentData.Cost);

            IncreaseAbilityEffect();
            IncreaseAbilityCost();

            UpgradeAbilityEvent.Invoke();

            JsonDataManagement.SaveData<SpaceShipAbilityPersistentData>(fileName: jsonFileName, data: spaceShipAbilityPersistentData);
        }
    }
}
