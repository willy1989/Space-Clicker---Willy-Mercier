using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAbilities_TurboShooting : SpaceShipAbilities
{
    private float turboShootingDuration;

    private float turboShootingMultiplier = 3f;

    protected override void DoAbility()
    {
        throw new System.NotImplementedException();
    }


    private IEnumerator startTurboShootingCoroutine()
    {
        yield return null;
    }

    protected override string GetCostPlayerPrefName()
    {
        return Constants.SpaceShipAbilityTurboCost_PlayerPref;
    }

    protected override string GetEffectPlayerPrefName()
    {
        return Constants.SpaceShipAbilityTurboEffect_PlayerPref;
    }
}
