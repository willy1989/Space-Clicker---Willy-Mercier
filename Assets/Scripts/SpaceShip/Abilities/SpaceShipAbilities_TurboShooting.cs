using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAbilities_TurboShooting : SpaceShipAbilities
{
    private const float effectDuration = 5f;

    private void Awake()
    {
        SetJasonFileName();

        SpaceShipAbilityPersistentData startJsonData = new SpaceShipAbilityPersistentData(_cost: spaceShipAbilityData.StartCost, _effect: spaceShipAbilityData.StartEffect);

        jsonDataUser = new JsonDataUser<SpaceShipAbilityPersistentData>(_StartJsonData: startJsonData, _jsonFileName: jsonFileName);
    }

    private void Start()
    {
        WaveManager.Instance.SpawnWaveAction += jsonDataUser.SaveData;
    }

    protected override void DoAbility()
    {
        StartCoroutine(StartTurboShootingCoroutine());
    }

    private IEnumerator StartTurboShootingCoroutine()
    {
        SpaceShipShooting spaceShipShooting = SpaceShipShooting.Instance;

        if (spaceShipShooting != null)
        {
            spaceShipShooting.ChangeFrequecyMultiplier(jsonDataUser.JsonData.Effect);
        }

        yield return new WaitForSeconds(effectDuration);

        if (spaceShipShooting != null)
        {
            spaceShipShooting.ChangeFrequecyMultiplier(1f);
        }
    }

    protected override void SetJasonFileName()
    {
        jsonFileName = "turboAbilityData.json";
    }
}
