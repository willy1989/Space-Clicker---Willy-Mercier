using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpaceShiplevelPersistentData
{
    public int SpaceShipLevel;

    public float SpaceShipXP;

    public float SpaceShipLevelThreshold;

    public float SpaceShipBaseDamage;

    public float SpaceShipBaseFrequency;

    public SpaceShiplevelPersistentData(int _spaceShipLevel, float _spaceShipXP, float _spaceShipLevelThreshold, float _spaceShipBaseDamage, float _spaceShipBaseFrequency)
    {
        SpaceShipLevel = _spaceShipLevel;
        SpaceShipXP = _spaceShipXP;
        SpaceShipLevelThreshold = _spaceShipLevelThreshold;
        SpaceShipBaseDamage = _spaceShipBaseDamage;
        SpaceShipBaseFrequency = _spaceShipBaseFrequency;
    }
}
