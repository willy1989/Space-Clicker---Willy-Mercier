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

    public void UpGradeDamage()
    {
        damage *= 2;
    }

    public void UpGradeFrequency()
    {
        shootingFrequency *= 1.2f;
    }
}
