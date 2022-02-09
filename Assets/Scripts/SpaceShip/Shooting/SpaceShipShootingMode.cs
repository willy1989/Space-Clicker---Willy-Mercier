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
             ProjectileMovement projectile = ProjectilesPoolManager.Instance.GetNextProjectile(_direction: Vector2.up, 
                                                                                               _damage: damage, 
                                                                                               _spawnPosition: shootPosition.position);

             SoundPlayer soundPlayer = projectile.gameObject.GetComponent<SoundPlayer>();

            if (soundPlayer != null)
                soundPlayer.PlaySoundEffect();
        }
    }
}

