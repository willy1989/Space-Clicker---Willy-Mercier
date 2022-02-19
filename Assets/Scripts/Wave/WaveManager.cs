using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] private Wave[] wavePrefabs;

    private const int startWaveIndex = 0;

    private const int spawnNextWaveDelay = 3;

    private WaveIndexPersistentData waveIndexPersistentData;

    private const string jsonFileName = "waveIndex.json";

    public int WaveCount
    {
        get
        {
            return waveIndexPersistentData.WaveIndex + 1;
        }
    }

    [SerializeField] Transform spawnPosition;

    public Wave CurrentWave { get; private set; }

    private void Awake()
    {
        SetInstance();
        LoadData();
    }

    private void LoadData()
    {
        if(JsonDataManagement.FileExists(jsonFileName) == true)
        {
            waveIndexPersistentData = JsonDataManagement.LoadData<WaveIndexPersistentData>(jsonFileName);
        }

        else
        {
            waveIndexPersistentData = new WaveIndexPersistentData(_waveIndex: startWaveIndex);
        }
    }

    public Target GetTargetWithMostHealth()
    {
        if (CurrentWave == null || CurrentWave.Targets.Count == 0)
            return null;

        Target targetWithMostHealth = CurrentWave.Targets[0];

        for (int i = 1; i < CurrentWave.Targets.Count; i++)
        {
            if (CurrentWave.Targets[i].CurrentHealth > targetWithMostHealth.CurrentHealth)
                targetWithMostHealth = CurrentWave.Targets[i];
        }

        return targetWithMostHealth;
    }

    public Target GetClosestTarget(Vector3 cannonPosition)
    {
        if (CurrentWave == null || CurrentWave.Targets.Count == 0)
            return null;

        Target closestTarget = CurrentWave.Targets[0];

        for (int i = 1; i < CurrentWave.Targets.Count; i++)
        {
            if ((CurrentWave.Targets[i].transform.position - cannonPosition).magnitude < 
                (closestTarget.transform.position - cannonPosition).magnitude)
                closestTarget = CurrentWave.Targets[i];
        }

        return closestTarget;
    }

    private void SpawnNextWave()
    {
        CurrentWave.LastTargetKilledEvent -= SpawnNextWave;

        WaveUI.Instance.UpdateCurrentWaveIcon();

        waveIndexPersistentData.WaveIndex++;

        JsonDataManagement.SaveData<WaveIndexPersistentData>(fileName: jsonFileName, data: waveIndexPersistentData);

        StartCurrentWave();
    }

    public void StartCurrentWave()
    {
        StartCoroutine(StartCurrentWaveCoroutine());
    }

    public IEnumerator StartCurrentWaveCoroutine()
    {
        WaveUI.Instance.ShowNextWaveText();

        yield return new WaitForSeconds(spawnNextWaveDelay);

        CurrentWave = Instantiate(wavePrefabs[waveIndexPersistentData.WaveIndex], spawnPosition.position, Quaternion.identity);

        CurrentWave.LastTargetKilledEvent += SpawnNextWave;
    }

    public void InterruptCurrentWave()
    {
        CurrentWave.LastTargetKilledEvent -= SpawnNextWave;

        Destroy(CurrentWave.gameObject);
    }
}
