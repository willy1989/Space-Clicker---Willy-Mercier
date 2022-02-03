using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] CannonUpgradeData cannonUpgradeData;

    private Target currentTarget;

    private bool shootToggle = true;

    private void Start()
    {
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while (shootToggle == true)
        {
            if (WaveManager.Instance.currentWave == null)
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

        Target nextTarget = WaveManager.Instance.GetTargetWithMostHealth();

        if (nextTarget != null)
        {
            currentTarget = nextTarget;
            currentTarget.DeathEvent += UnlockTarget;
        }
    }

    private void SpawnProjectile()
    {
        if (currentTarget == null)
            return;

        GameObject projectile = Instantiate(projectilePrefab, (Vector2)transform.position + Vector2.up / 2, Quaternion.identity);

        // TO DO: Add exception, the projectile must have a ProjectileMovement component

        ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>();

        projectileMovement.SetDamage(_damage: cannonUpgradeData.Damage);
        projectileMovement.SetDirection(_target: currentTarget);
    }

    private void UnlockTarget()
    {
        currentTarget.DeathEvent -= LockOnToTarget;
    }
}



