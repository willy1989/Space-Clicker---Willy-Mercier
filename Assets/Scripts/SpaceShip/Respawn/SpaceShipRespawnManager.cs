using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipRespawnManager : Singleton<SpaceShipRespawnManager>
{
    [SerializeField] private Transform spawnPosition;

    [SerializeField] private GameObject spaceShipPrefab;

    private SpaceShipDamage spaceShipDamage;

    public int RespawnDelay { get; private set; } = 5;

    private void Awake()
    {
        SetInstance();
    }

    private void Start()
    {
        SpawnSpaceShip();
    }

    public void RespawnSpaceShip()
    {
        StartCoroutine(RespawnSpaceShipCoroutine());
    }

    private void SpawnSpaceShip()
    {
        GameObject spaceShip = Instantiate(spaceShipPrefab, spawnPosition.position, Quaternion.identity);
        spaceShipDamage = spaceShip.GetComponentInChildren<SpaceShipDamage>();
        spaceShip.GetComponentInChildren<SpaceShipDamage>().BecomeInvincible();

        if (spaceShipDamage != null)
            spaceShipDamage.DeathEvent += RespawnSpaceShip;
    }

    private IEnumerator RespawnSpaceShipCoroutine()
    {
        if (spaceShipDamage != null)
            Destroy(spaceShipDamage.transform.parent.gameObject);

        SpaceShipRespawnUI.Instance.StartRespawnCountDownText(RespawnDelay);

        yield return new WaitForSeconds(RespawnDelay);

        SpawnSpaceShip();
    }

    
}
