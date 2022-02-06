using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnManager : Singleton<PowerUpSpawnManager>
{
    [SerializeField] private GameObject[] powerUpPrefabs;

    private void Awake()
    {
        SetInstance();
    }

    public GameObject GetRandomPowerUpPrefab()
    {
        int randomNumber = Random.Range(0, powerUpPrefabs.Length);

        return powerUpPrefabs[randomNumber];
    }
}
