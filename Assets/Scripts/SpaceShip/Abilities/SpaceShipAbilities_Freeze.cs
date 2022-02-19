using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAbilities_Freeze : SpaceShipAbilities
{
    private void Awake()
    {
        SetJasonFileName();
        LoadData();
    }


    protected override void DoAbility()
    {
        StartCoroutine(FreezeAllTargets());
    }

    private IEnumerator FreezeAllTargets()
    {
        Target.CanMove = false;

        yield return new WaitForSeconds(spaceShipAbilityPersistentData.Effect);

        Target.CanMove = true;
    }

    protected override void SetJasonFileName()
    {
        jsonFileName = "FreezeAbilityData.json";
    }
}
