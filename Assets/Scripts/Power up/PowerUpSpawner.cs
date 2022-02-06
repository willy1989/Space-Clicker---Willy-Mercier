using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Target))]
public class PowerUpSpawner : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] private int spawnPowerUpChance;

    private void Awake()
    {
        GetComponent<Target>().DeathEvent += SpawnPowerUp;
    }

    private void SpawnPowerUp()
    {
        int randomNumber = Random.Range(1, 101);

        if (randomNumber <= spawnPowerUpChance)
            Instantiate(PowerUpSpawnManager.Instance.GetRandomPowerUpPrefab(), transform.position, Quaternion.identity);
    }
}
