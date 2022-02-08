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

    private float shootingRange = 14f;

    private float damagePowerUp = 1f;

    private float frequencyPowerUp = 1f;

    private float frequencyPowerUpIncreaseRate = 1.2f;

    private float frequencyMultiplier = 1f;

    private float damagePowerUpIncreaseRate = 2f;

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
            yield return new WaitForSeconds((SpaceShipLevelManager.Instance.SpaceShipBaseFrequency / frequencyPowerUp) / frequencyMultiplier);
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
            spaceShipShootingMode[shootingModeIndex].Shoot(projectilePrefab, SpaceShipLevelManager.Instance.SpaceShipBaseDamage * damagePowerUp);
    }

    public void ChangeFrequecyMultiplier(float multiplier)
    {
        frequencyMultiplier = multiplier;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.ShootingModePowerUp_Tag) == true)
        {
            shootingModeIndex++;
            PowerUpUI.Instance.ShowPowerUpMessage(Constants.PowerUpMessageMoreGuns);
            Destroy(collision.gameObject);
        }

        else if(collision.CompareTag(Constants.ShootingDamagePowerUp_Tag) == true)
        {
            damagePowerUp *= damagePowerUpIncreaseRate;
            PowerUpUI.Instance.ShowPowerUpMessage(Constants.PowerUpMessageMoreDamage);
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag(Constants.ShootingFrequencyPowerUp_Tag) == true)
        {
            frequencyPowerUp *= frequencyPowerUpIncreaseRate;
            PowerUpUI.Instance.ShowPowerUpMessage(Constants.PowerUpMessageFrequency);
            Destroy(collision.gameObject);
        }
    }
}
