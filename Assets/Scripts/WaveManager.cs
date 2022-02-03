using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] Wave[] wavePrefabs;

    int waveIndex = 0;

    [SerializeField] Transform spawnPosition;

    public Wave currentWave { get; private set; }

    private void Awake()
    {
        SetInstance();
    }

    private void Start()
    {
        currentWave = Instantiate(wavePrefabs[waveIndex], spawnPosition.position, Quaternion.identity);

        currentWave.LastTargetKilledEvent += SwitchToNextWave;
    }

    public Target GetTargetWithMostHealth()
    {
        if (currentWave.Targets.Count == 0)
            return null;

        Target targetWithMostHealth = currentWave.Targets[0];

        for (int i = 1; i < currentWave.Targets.Count; i++)
        {
            if (currentWave.Targets[i].CurrentHealth > targetWithMostHealth.CurrentHealth)
                targetWithMostHealth = currentWave.Targets[i];
        }

        return targetWithMostHealth;
    }

    private void SwitchToNextWave()
    {
        currentWave.LastTargetKilledEvent -= SwitchToNextWave;

        waveIndex++;

        currentWave = Instantiate(wavePrefabs[waveIndex], spawnPosition.position, Quaternion.identity);

        currentWave.LastTargetKilledEvent += SwitchToNextWave;
    }


    

}
