using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesPoolManager : Singleton<ProjectilesPoolManager>
{
    [SerializeField] private ProjectileMovement projectilePrefab;

    private const int poolSize = 300;

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

    public ProjectileMovement GetNextProjectile()
    {
        poolIndex++;

        poolIndex %= poolSize;

        pool[poolIndex].gameObject.SetActive(true);

        return pool[poolIndex];
    }
}
