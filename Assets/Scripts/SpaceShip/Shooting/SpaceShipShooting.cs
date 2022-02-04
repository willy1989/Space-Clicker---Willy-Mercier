using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipShooting : MonoBehaviour
{
    [SerializeField] private ProjectileMovement projectilePrefab;

    [SerializeField] private SpaceShipShootingMode spaceShipShootingMode;

    private int damage = 1;

    private int shootFrequency = 1;

    private bool canShoot = true;

    private void Start()
    {
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while(canShoot == true)
        {
            Shoot();
            yield return new WaitForSeconds(shootFrequency);
        }
    }

    private void Shoot()
    {
        if (spaceShipShootingMode == null)
            return;

        spaceShipShootingMode.Shoot(projectilePrefab, damage);
    }
}
