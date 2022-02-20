using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipShooting : Singleton<SpaceShipShooting>
{
    [SerializeField] private ProjectileMovement projectilePrefab;

    [SerializeField] private SoundPlayer powerUpSoundPlayer;

    [SerializeField] private SoundPlayer shootingSoundPlayer;

    SpaceShipShootingMode[] spaceShipShootingMode;

    private int ShootingModeIndex
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
    private float damagePowerUpIncreaseRate = 2f;


    private float frequencyPowerUpIncreaseRate = 1.2f;

    private float _frequencyMultiplier = 1f;

    private float FrequencyMultiplier
    {
        get
        {
            return _frequencyMultiplier;
        }

        set
        {
            if (value > maxFrequencyMultiplier)
                _frequencyMultiplier = maxFrequencyMultiplier;
            else
                _frequencyMultiplier = value;
        }
    }

    private const float maxFrequencyMultiplier = 5f;

    private float FrequencyPowerUp
    {
        get
        {
            return _frequencyPowerUp;
        }

        set
        {
            if (value > maxFrequencyPowerUp)
                _frequencyPowerUp = maxFrequencyPowerUp;
            else
                _frequencyPowerUp = value;
        }
    }

    private float _frequencyPowerUp = 1f;

    private const float maxFrequencyPowerUp = 5f;


    private bool canShoot = true;

    private void Awake()
    {
        SetInstance();

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
            yield return new WaitForSeconds((SpaceShipLevelManager.Instance.SpaceShipBaseFrequency / FrequencyPowerUp) / FrequencyMultiplier);
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
        {
            spaceShipShootingMode[ShootingModeIndex].Shoot(projectilePrefab, SpaceShipLevelManager.Instance.SpaceShipBaseDamage * damagePowerUp);
            shootingSoundPlayer.PlaySoundEffect();
        }
            
    }

    public void ChangeFrequecyMultiplier(float multiplier)
    {
        FrequencyMultiplier = multiplier;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.ShootingModePowerUp_Tag) == true)
        {
            CollectPowerUp(powerUpEffect : () => ShootingModeIndex++, 
                           message: Constants.PowerUpMessageMoreGuns, 
                           powerUp: collision.gameObject);
        }

        else if(collision.CompareTag(Constants.ShootingDamagePowerUp_Tag) == true)
        {
            CollectPowerUp(powerUpEffect: () => damagePowerUp *= damagePowerUpIncreaseRate,
                           message: Constants.PowerUpMessageMoreDamage,
                           powerUp: collision.gameObject);
        }

        else if (collision.CompareTag(Constants.ShootingFrequencyPowerUp_Tag) == true)
        {
            CollectPowerUp(powerUpEffect: () => FrequencyPowerUp *= frequencyPowerUpIncreaseRate,
                           message: Constants.PowerUpMessageFrequency,
                           powerUp: collision.gameObject);
        }
    }

    private void CollectPowerUp(Action powerUpEffect, string message, GameObject powerUp)
    {
        powerUpEffect.Invoke();
        PowerUpUI.Instance.ShowPowerUpMessage(message);
        powerUpSoundPlayer.PlaySoundEffect();
        Destroy(powerUp);
    }
}
