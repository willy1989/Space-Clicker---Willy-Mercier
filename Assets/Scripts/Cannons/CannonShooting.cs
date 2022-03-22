using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundPlayer))]
public class CannonShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;

    private SoundPlayer soundPlayer;

    CannonUpgradeData cannonUpgradeData;

    private Target currentTarget;

    private int shootingRange = 14;

    private bool shootToggle = true;


    private void Awake()
    {
        cannonUpgradeData = GetComponent<CannonUpgradeData>();
        soundPlayer = GetComponent<SoundPlayer>();
    }

    private void Start()
    {
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while (shootToggle == true)
        {
            if (WaveManager.Instance.CurrentWave == null)
                yield return null;

            LockOnToTarget();

            SpawnProjectile();

            yield return new WaitForSeconds(cannonUpgradeData.ShootingFrequency);
        }
    }

    private void LockOnToTarget()
    {
        if (currentTarget != null)
            return;

        Target nextTarget = WaveManager.Instance.GetClosestTarget(transform.position);

        if (nextTarget != null && TargetWithingRange(nextTarget) == true)
        {
            currentTarget = nextTarget;
            currentTarget.DeathEvent += UnlockTarget;
        }
    }

    private void SpawnProjectile()
    {
        if (currentTarget == null)
            return;

        Vector2 direction = currentTarget.transform.position - transform.position;

        Vector2 spawnPosition = ((Vector2)transform.position + Vector2.up / 2);

        ProjectileMovement projectileMovement = ProjectilesPoolManager.Instance.GetNextProjectile(_direction: direction, 
                                                                                                  _damage: cannonUpgradeData.Damage, 
                                                                                                  _spawnPosition: spawnPosition);

        soundPlayer.PlaySoundEffect();
    }

    private void UnlockTarget()
    {
        currentTarget.DeathEvent -= LockOnToTarget;
    }

    private bool TargetWithingRange(Target target)
    {
        return (target.transform.position - transform.position).magnitude <= shootingRange;
    }
}



