using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAbilities_TurboShooting : SpaceShipAbilities
{
    private float effectDuration = 5f;

    protected override void DoAbility()
    {
        StartCoroutine(startTurboShootingCoroutine());
    }

    private IEnumerator startTurboShootingCoroutine()
    {
        SpaceShipShooting shapShipShooting = FindObjectOfType<SpaceShipShooting>();

        if (shapShipShooting != null)
        {
            shapShipShooting.ChangeFrequecyMultiplier(Effect);
        }

        yield return new WaitForSeconds(effectDuration);

        if (shapShipShooting != null)
        {
            shapShipShooting.ChangeFrequecyMultiplier(1f);
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
