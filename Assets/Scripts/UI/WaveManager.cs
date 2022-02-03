using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] Wave[] wavePrefabs;

    private int waveIndex = 0;

    public int WaveCount
    {
        get
        {
            return waveIndex + 1;
        }
    }

    [SerializeField] Transform spawnPosition;

    public Wave currentWave { get; private set; }

    private void Awake()
    {
        SetInstance();
    }

    public Target GetTargetWithMostHealth()
    {
        if (currentWave == null || currentWave.Targets.Count == 0)
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

        WaveUI.Instance.UpdateCurrentWaveIcon();

        waveIndex++;

        StartCurrentWave();
    }

    public void StartCurrentWave()
    {
        currentWave = Instantiate(wavePrefabs[waveIndex], spawnPosition.position, Quaternion.identity);

        currentWave.LastTargetKilledEvent += SwitchToNextWave;

        WaveUI.Instance.ShowNextWaveText();
    }

    public void InterruptCurrentWave()
    {
        currentWave.LastTargetKilledEvent -= SwitchToNextWave;

        Destroy(currentWave.gameObject);

        currentWave = null;
    }

}
