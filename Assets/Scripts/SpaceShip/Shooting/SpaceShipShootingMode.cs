using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceShipShootingMode : MonoBehaviour
{
    public abstract void Shoot(ProjectileMovement projectileMovement, int damage);
}
