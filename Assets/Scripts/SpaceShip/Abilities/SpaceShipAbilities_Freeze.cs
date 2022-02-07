using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAbilities_Freeze : SpaceShipAbilities
{
    protected override void DoAbility()
    {
        StartCoroutine(freezeAllTargets());
    }

    private IEnumerator freezeAllTargets()
    {
        foreach(Target target in WaveManager.Instance.CurrentWave.Targets)
        {
            target.ToggleMovement();
        }

        yield return new WaitForSeconds(Effect);

        foreach (Target target in WaveManager.Instance.CurrentWave.Targets)
        {
            target.ToggleMovement();
        }
    }


    protected override string GetEffectPlayerPrefName()
    {
        return Constants.SpaceShipAbilityFreezeEffect_PlayerPref;
    }

    protected override string GetCostPlayerPrefName()
    {
        return Constants.SpaceShipAbilityFreezeCost_PlayerPref;
    }

    
}
