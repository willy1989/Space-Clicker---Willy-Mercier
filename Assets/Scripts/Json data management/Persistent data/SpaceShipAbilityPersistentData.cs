using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpaceShipAbilityPersistentData
{
    public float Cost;

    public float Effect;

    public SpaceShipAbilityPersistentData(float _cost, float _effect)
    {
        Cost = _cost;
        Effect = _effect;
    }
}
