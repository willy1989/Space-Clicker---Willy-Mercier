using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cannon data", menuName = "ScriptableObjects/Cannon data", order = 1)]
public class CannonUpgradeData:ScriptableObject
{
    [SerializeField] private float shootingFrequency;

    public float ShootingFrequency
    {  
        get
        {
            return shootingFrequency;
        }
    }

    [SerializeField] int damage;

    public int Damage
    {
        get
        {
            return damage;
        }
    }

    [SerializeField] private float nextDamageUpGradeCost;

    public float NextDamageUpGradeCost
    {
        get
        {
            return nextDamageUpGradeCost;
        }
    }

    [SerializeField] private float nextfrequencyUpGradeCost;

    public float NextFrequencyUpGradeCost
    {
        get
        {
            return nextfrequencyUpGradeCost;
        }
    }

    public void UpGradeDamage()
    {
        if (CurrencyManager.Instance.HasSufficientBalance(nextDamageUpGradeCost) == false)
            return;

        CurrencyManager.Instance.SpendCurrency(nextDamageUpGradeCost);

        damage *= 2;
        nextDamageUpGradeCost *= 2;
    }

    public void UpGradeFrequency()
    {
        if (CurrencyManager.Instance.HasSufficientBalance(nextfrequencyUpGradeCost) == false)
            return;

        CurrencyManager.Instance.SpendCurrency(nextfrequencyUpGradeCost);

        shootingFrequency /= 1.2f;
        nextfrequencyUpGradeCost *= 1.2f;
    }
}
