using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShooting : MonoBehaviour
{
    [SerializeField] private TargetsTracker targetTracker;

    [SerializeField] private GameObject projectilePrefab;

    private Target currentTarget;

    private float shootingFrequency = 1f;

    private int damage = 1;

    private bool shootToggle = true;

    private void Start()
    {
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while (shootToggle == true)
        {
            LockOnToTarget();

            SpawnProjectile();

            yield return new WaitForSeconds(shootingFrequency);
        }
    }

    // Pick a new target, the one with the most health, and shoot it until it's dead.
    private void LockOnToTarget()
    {
        if (currentTarget != null)
            return;

        Target nextTarget = targetTracker.GetTargetWithMostHealth();

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

        projectileMovement.SetDamage(_damage: damage);
        projectileMovement.SetDirection(_target: currentTarget);
    }

    private void UnlockTarget()
    {
        currentTarget.DeathEvent -= LockOnToTarget;
    }
}



