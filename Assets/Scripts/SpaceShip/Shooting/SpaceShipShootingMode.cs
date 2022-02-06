using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipShootingMode : MonoBehaviour
{
    [SerializeField] private Transform[] shootPositions;

    public void Shoot(ProjectileMovement projectileMovementPrefab, float damage)
    {
        foreach(Transform shootPosition in shootPositions)
        {
             ProjectileMovement projectile = ProjectilesPoolManager.Instance.GetNextProjectile();

             projectile.SetDamage(damage);

             projectile.SetDirection(Vector2.up);

            projectile.SetSpawnPosition(shootPosition.position);
        }
    }
}

