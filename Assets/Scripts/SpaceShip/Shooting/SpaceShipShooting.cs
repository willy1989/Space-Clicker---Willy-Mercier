using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipShooting : MonoBehaviour
{
    [SerializeField] private ProjectileMovement projectilePrefab;

    SpaceShipShootingMode[] spaceShipShootingMode;

    private int shootingModeIndex
    {
        get
        {
            return _shootingModeIndex;
        }

        set
        {
            if (value >= 0 && value < spaceShipShootingMode.Length)
                _shootingModeIndex = value;
        }
    }

    private int _shootingModeIndex = 0;


    private int damage = 1;

    private float shootFrequency = 0.5f;

    private float shootingRange = 15f;

    private bool canShoot = true;

    private void Awake()
    {
        spaceShipShootingMode = GetComponentsInChildren<SpaceShipShootingMode>();
    }

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

        Target closestTarget = WaveManager.Instance.GetClosestTarget(transform.position);

        if (closestTarget == null)
            return;

        if((closestTarget.transform.position - transform.position).magnitude <= shootingRange)
            spaceShipShootingMode[shootingModeIndex].Shoot(projectilePrefab, damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.ShootingModePowerUp_Tag) == true)
        {
            shootingModeIndex++;
            Destroy(collision.gameObject);
        }

        else if(collision.CompareTag(Constants.ShootingDamagePowerUp_Tag) == true)
        {
            damage *= 2;
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag(Constants.ShootingFrequencyPowerUp_Tag) == true)
        {
            shootFrequency /= 2;
            Destroy(collision.gameObject);
        }
    }
}
