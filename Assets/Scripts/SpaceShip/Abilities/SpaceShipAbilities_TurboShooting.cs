using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAbilities_TurboShooting : SpaceShipAbilities
{
    private const float effectDuration = 5f;

    protected override void DoAbility()
    {
        StartCoroutine(StartTurboShootingCoroutine());
    }

    private IEnumerator StartTurboShootingCoroutine()
    {
        SpaceShipShooting spaceShipShooting = SpaceShipShooting.Instance;

        if (spaceShipShooting != null)
        {
            spaceShipShooting.ChangeFrequecyMultiplier(Effect);
        }

        yield return new WaitForSeconds(effectDuration);

        if (spaceShipShooting != null)
        {
            spaceShipShooting.ChangeFrequecyMultiplier(1f);
        }
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
