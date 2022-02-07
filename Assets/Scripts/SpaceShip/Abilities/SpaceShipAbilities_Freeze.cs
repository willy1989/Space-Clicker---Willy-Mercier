using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAbilities_Freeze : SpaceShipAbilities
{
    protected override void DoAbility()
    {
        throw new System.NotImplementedException();
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
