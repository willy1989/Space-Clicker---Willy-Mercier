using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] private Transform spawnFirstRowPosition;

    [SerializeField] private WaveRowGroup[] waveRowGroups;

    private const int spawnNextWaveDelay = 2;

    public int WaveCount
    {
        get
        {
            return jsonDataUser_Wave.JsonData.WaveIndex + 1;
        }
    }

    public Wave CurrentWave { get; private set; }

    public Action SpawnWaveAction;

    private bool succeededWave = false;

    private float spaceBetweenRows = 10f;

    private float startBudget = 400f;

    private bool startBudgetOverride = false;

    private float budgetIncreaseRate = 1.2f;

    private JsonDataUser<WavePersistentData> jsonDataUser_Wave;

    private void Awake()
    {
        SetInstance();

        WavePersistentData wavePersistentData = new WavePersistentData(_WaveIndex: 0, _Budget: startBudget);

        jsonDataUser_Wave = new JsonDataUser<WavePersistentData>(_StartJsonData: wavePersistentData, "WaveData.json");
    }

    private void Update()
    {
        if(CurrentWave != null && CurrentWave.Targets.Count <= 0 )
        {
            InterruptCurrentWave();
            SpawnNewWave();

            jsonDataUser_Wave.JsonData.WaveIndex++;
            WaveUI.Instance.UpdateCurrentWaveIcon();
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

    public void SpawnNewWave()
    {
        StartCoroutine(SpawnNewWaveCoroutine());
    }

    private IEnumerator SpawnNewWaveCoroutine()
    {
        yield return new WaitForSeconds(spawnNextWaveDelay);

        CurrentWave = SpawnWave();

        WaveUI.Instance.ShowNextWaveText();

        if(succeededWave == true)
            IncreaseBudget();

        succeededWave = true;

        jsonDataUser_Wave.SaveData();

        if (SpawnWaveAction != null)
            SpawnWaveAction.Invoke();
    }

    /// <summary>
    /// Wave are procedurally generated. Each wave is made of rows, which contains enemies.
    /// Rows are split in different categories, based on their price. 
    /// Rows contain targets (enemies).
    /// To spawn a new wave, the wave manager has a fixed budget to spend on rows.
    /// The budget grows with each sucessfully completed wave.
    /// </summary>
    
    public Wave SpawnWave()
    {
        List<WaveRowPrefab> rows = SelectRows();

        Wave wave = new Wave();

        Vector3 spawnPosition = spawnFirstRowPosition.position;

        // Rows are spawned from cheapest to most expensive
        for (int i = rows.Count - 1; i >= 0; i--)
        {
            WaveRowPrefab spawnRow = Instantiate(rows[i], spawnPosition, Quaternion.identity);

            wave.AddTargets(spawnRow.Targets);

            spawnPosition += new Vector3(0f, spaceBetweenRows, 0f);
        }

        return wave;
    }

    /// <summary>
    /// The algorithm always tries to buy as many rows as possible from the most expensive category first.
    /// When it can't afford rows from that category, it then moves on to the next cheaper one, 
    /// and so forth and so on until it can't afford any row anymore. 
    /// When a row category is picked, a random row is selected from it.
    /// /// </summary>
    private List<WaveRowPrefab> SelectRows()
    {
        List<WaveRowPrefab> selectedPrefabs = new List<WaveRowPrefab>();

        float budgetLeft;

        if (startBudgetOverride == true)
            budgetLeft = 400f;

        else
            budgetLeft = jsonDataUser_Wave.JsonData.Budget;

        int rowGroupIndex = 0;

        while (budgetLeft > 0 && rowGroupIndex < waveRowGroups.Length)
        {
            if (budgetLeft >= waveRowGroups[rowGroupIndex].Cost)
            {
                WaveRowPrefab waveRowPrefab = waveRowGroups[rowGroupIndex].GetRandomRow();

                selectedPrefabs.Add(waveRowPrefab);

                budgetLeft -= waveRowGroups[rowGroupIndex].Cost;
            }

            else
            {
                rowGroupIndex++;
            }
        }

        return selectedPrefabs;
    }

    public void FailWave()
    {
        succeededWave = false;
    }

    public void InterruptCurrentWave()
    {
        foreach(Target target in CurrentWave.Targets)
        {
            Destroy(target.gameObject);
        }

        CurrentWave = null; 
    }

    public void IncreaseBudget()
    {
        if (startBudgetOverride == true)
            return;

        jsonDataUser_Wave.JsonData.Budget *= budgetIncreaseRate;
    }
}
