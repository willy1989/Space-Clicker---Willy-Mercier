using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipRespawnManager : Singleton<SpaceShipRespawnManager>
{
    [SerializeField] private Transform spawnPosition;

    [SerializeField] private GameObject spaceShipPrefab;

    [SerializeField] private ParticleSystem deathParticleSystem;

    [SerializeField] private SoundPlayer soundPlayer;

    private SpaceShipDamage spaceShipDamage;

    private GameObject spaceShip;

    public int RespawnDelay { get; private set; } = 5;

    private void Awake()
    {
        SetInstance();
    }

    private void Start()
    {
        SpawnSpaceShip();
    }

    

    private void SpawnSpaceShip()
    {
        spaceShip = Instantiate(spaceShipPrefab, spawnPosition.position, Quaternion.identity);
        spaceShipDamage = spaceShip.GetComponentInChildren<SpaceShipDamage>();
        spaceShipDamage.BecomeInvincible();

        if (spaceShipDamage != null)
            spaceShipDamage.DeathEvent += RespawnSpaceShip;
    }

    public void RespawnSpaceShip()
    {
        StartCoroutine(RespawnSpaceShipCoroutine());
    }

    private IEnumerator RespawnSpaceShipCoroutine()
    {
        if (spaceShipDamage == null)
            yield break;

        Destroy(spaceShip);

        soundPlayer.PlaySoundEffect();

        deathParticleSystem.transform.position = spaceShip.transform.position;

        deathParticleSystem.Play();

        SpaceShipRespawnUI.Instance.StartRespawnCountDownText(RespawnDelay);

        yield return new WaitForSeconds(RespawnDelay);

        SpawnSpaceShip();
    }

    
}
