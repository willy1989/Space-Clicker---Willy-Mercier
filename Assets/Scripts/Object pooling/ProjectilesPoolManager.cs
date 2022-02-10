using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesPoolManager : Singleton<ProjectilesPoolManager>
{
    [SerializeField] private ProjectileMovement projectilePrefab;

    private const int poolSize = 500;

    private ProjectileMovement[] pool = new ProjectileMovement[poolSize];

    private int poolIndex = -1;

    private void Awake()
    {
        SetInstance();
        PopulatePool();
    }

    private void PopulatePool()
    {
        for(int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(projectilePrefab, Vector2.zero, Quaternion.identity);

            pool[i].gameObject.SetActive(false);
        }
    }

    public ProjectileMovement GetNextProjectile(Vector2 _direction, float _damage, Vector2 _spawnPosition)
    {
        poolIndex++;

        poolIndex %= poolSize;

        ProjectileMovement projectile = pool[poolIndex];

        projectile.gameObject.SetActive(true);

        projectile.SetData(_direction, _damage, _spawnPosition);

        return pool[poolIndex];
    }
}
