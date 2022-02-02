using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsTracker : MonoBehaviour
{
    [SerializeField] private List<Target> targetsInGame;

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
