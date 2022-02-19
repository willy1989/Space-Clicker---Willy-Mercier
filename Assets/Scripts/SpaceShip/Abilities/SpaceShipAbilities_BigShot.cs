using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAbilities_BigShot : SpaceShipAbilities
{
    [SerializeField] private BigShot bigShotPrefab;

    private Vector3 shootingPositionOffset = new Vector3(0f, 4f, 0f);

    private void Awake()
    {
        SetJasonFileName();
        LoadData();
    }

    protected override void DoAbility()
    {
        SpaceShipShooting spaceShipShooting = FindObjectOfType<SpaceShipShooting>();

        if (spaceShipShooting == null)
            return;

        BigShot bigShot = Instantiate(bigShotPrefab, spaceShipShooting.transform.position + shootingPositionOffset, Quaternion.identity);

        bigShot.SetDamage(spaceShipAbilityPersistentData.Effect);
    }

    protected override void SetJasonFileName()
    {
        jsonFileName = "bigShotAbilityData.json";
    }
}
