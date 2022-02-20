using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] private Wave[] wavePrefabs;

    private const int startWaveIndex = 0;

    private const int spawnNextWaveDelay = 3;

    private JsonDataUser<WaveIndexPersistentData> jsonDataUser;

    public int WaveCount
    {
        get
        {
            return jsonDataUser.JsonData.WaveIndex + 1;
        }
    }

    [SerializeField] Transform spawnPosition;

    public Wave CurrentWave { get; private set; }

    private void Awake()
    {
        SetInstance();

        WaveIndexPersistentData waveIndexPersistentData = new WaveIndexPersistentData(_waveIndex: 0);

        jsonDataUser = new JsonDataUser<WaveIndexPersistentData>(_StartJsonData: waveIndexPersistentData, _jsonFileName: "waveIndexData.json");
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

        jsonDataUser.JsonData.WaveIndex++;

        jsonDataUser.SaveData();

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

        CurrentWave = Instantiate(wavePrefabs[jsonDataUser.JsonData.WaveIndex], spawnPosition.position, Quaternion.identity);

        CurrentWave.LastTargetKilledEvent += SpawnNextWave;
    }

    public void InterruptCurrentWave()
    {
        CurrentWave.LastTargetKilledEvent -= SpawnNextWave;

        Destroy(CurrentWave.gameObject);
    }
}
