using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipShootingMode : MonoBehaviour
{
    [SerializeField] private Transform[] shootPositions;

    public void Shoot(ProjectileMovement projectileMovementPrefab, int damage)
    {
        foreach(Transform shootPosition in shootPositions)
        {
             ProjectileMovement projectile = Instantiate(projectileMovementPrefab, shootPosition.position, Quaternion.identity);

             projectile.SetDamage(damage);

             projectile.SetDirection(Vector2.up);
        }
    }
}

