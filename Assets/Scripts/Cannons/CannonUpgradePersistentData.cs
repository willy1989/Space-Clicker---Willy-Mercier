using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CannonUpgradePersistentData
{
    public float ShootingFrequency;

    public int Damage;

    public float NextDamageUpGradeCost;

    public float NextFrequencyUpGradeCost;

    public CannonUpgradePersistentData(float _ShootingFrequency, int _Damage, float _NextDamageUpGradeCost, float _NextFrequencyUpGradeCost)
    {
        ShootingFrequency = _ShootingFrequency;
        Damage = _Damage;
        NextDamageUpGradeCost = _NextDamageUpGradeCost;
        NextFrequencyUpGradeCost = _NextFrequencyUpGradeCost;
    }

    public CannonUpgradePersistentData()
    {
        ;
    }
}
