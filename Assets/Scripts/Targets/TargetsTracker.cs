using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsTracker : Singleton<TargetsTracker>
{
    [SerializeField] private List<Target> targetsInGame;

    private void Awake()
    {
        SetInstance();
    }

    public Target GetTargetWithMostHealth()
    {
        Target targetWithMostHealth = targetsInGame[0];

        for(int i = 1; i < targetsInGame.Count; i++)
        {
            if (targetsInGame[i].CurrentHealth > targetWithMostHealth.CurrentHealth)
                targetWithMostHealth = targetsInGame[i];
        }

        return targetWithMostHealth;
    }
}
