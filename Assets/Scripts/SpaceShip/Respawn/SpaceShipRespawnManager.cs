using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipRespawnManager : Singleton<SpaceShipRespawnManager>
{
    [SerializeField] private Transform spawnPosition;

    [SerializeField] private GameObject spaceShipPrefab;

    [SerializeField] private ParticleSystem deathParticleSystem;

    [SerializeField] private SoundPlayer soundPlayer;

    private GameObject spaceShip;

    public int RespawnDelay { get; private set; } = 5;

    private void Awake()
    {
        SetInstance();
    }

    public void SpawnSpaceShip()
    {
        if (spaceShip != null)
            return;

        spaceShip = Instantiate(spaceShipPrefab, spawnPosition.position, Quaternion.identity);
    }

    public void DespawnSpaceShip()
    {
        if (spaceShip == null)
            return;

        Destroy(spaceShip);

        soundPlayer.PlaySoundEffect();

        deathParticleSystem.transform.position = spaceShip.transform.position;

        deathParticleSystem.Play();
    }

    public void RespawnSpaceShip()
    {
        StartCoroutine(RespawnSpaceShipCoroutine());
    }

    private IEnumerator RespawnSpaceShipCoroutine()
    {
        SpaceShipRespawnUI.Instance.StartRespawnCountDownText(RespawnDelay);

        yield return new WaitForSeconds(RespawnDelay);

        SpawnSpaceShip();
    }
}
