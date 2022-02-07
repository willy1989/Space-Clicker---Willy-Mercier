using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] CannonUpgradeData cannonUpgradeData;

    private Target currentTarget;

    private int shootingRange = 20;

    private bool shootToggle = true;

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

    // Pick a new target, the one with the most health, and shoot it until it's dead.
    private void LockOnToTarget()
    {
        if (currentTarget != null)
            return;

        //Target nextTarget = WaveManager.Instance.GetTargetWithMostHealth();

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

        ProjectileMovement projectileMovement = ProjectilesPoolManager.Instance.GetNextProjectile();

        projectileMovement.SetDamage(_damage: cannonUpgradeData.Damage);

        projectileMovement.SetSpawnPosition((Vector2)transform.position + Vector2.up / 2);

        projectileMovement.SetDirection(_target: currentTarget); 
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



