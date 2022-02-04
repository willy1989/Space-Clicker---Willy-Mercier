using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipShootingMode_Pea : SpaceShipShootingMode
{
    public override void Shoot(ProjectileMovement projectileMovementPrefab, int damage)
    {
        ProjectileMovement projectile = Instantiate(projectileMovementPrefab, (Vector2)transform.position, Quaternion.identity);

        projectile.SetDamage(damage);

        projectile.SetDirection(Vector2.up);
    }
}
