using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Space ship abilities data", menuName = "ScriptableObjects/Space ship abilities data", order = 1)]
public class SpaceShipAbilitiesData : ScriptableObject
{
    [Range(1,30)]
    [SerializeField] private int coolDownDuration;

    [Range(1.01f, 2f)]
    [SerializeField] private float effectIncreaseRate;

    [Range(1.01f, 2f)]
    [SerializeField] private float costIncreaseRate;

    [Range(1f, 1000f)]
    [SerializeField] private float startEffect;

    [Range(1f, 1000f)]
    [SerializeField] private float startCost;

    public int CoolDownDuration
    {
        get
        {
            return coolDownDuration;
        }
    }

    public float EffectIncreaseRate
    {
        get
        {
            return effectIncreaseRate;
        }
    }

    public float CostIncreaseRate
    {
        get
        {
            return costIncreaseRate;
        }
    }

    public float StartEffect
    {
        get
        {
            return startEffect;
        }
    }

    public float StartCost
    {
        get
        {
            return startCost;
        }
    }
}
