using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAbilities_Freeze : SpaceShipAbilities
{
    private void Awake()
    {
        SetJasonFileName();

        SpaceShipAbilityPersistentData startJsonData = new SpaceShipAbilityPersistentData(_cost: spaceShipAbilityData.StartCost, _effect: spaceShipAbilityData.StartEffect);

        jsonDataUser = new JsonDataUser<SpaceShipAbilityPersistentData>(_StartJsonData: startJsonData, _jsonFileName: jsonFileName);
    }


    protected override void DoAbility()
    {
        StartCoroutine(FreezeAllTargets());
    }

    private IEnumerator FreezeAllTargets()
    {
        Target.CanMove = false;

        yield return new WaitForSeconds(jsonDataUser.JsonData.Effect);

        Target.CanMove = true;
    }

    protected override void SetJasonFileName()
    {
        jsonFileName = "FreezeAbilityData.json";
    }
}
