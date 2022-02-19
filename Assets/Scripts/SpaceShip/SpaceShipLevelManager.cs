using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipLevelManager : Singleton<SpaceShipLevelManager>
{
    private const int startLevel = 1;

    private const int startXP = 0;

    private const float startLevelThreshold = 30f;

    private const float startDamage = 1;

    private const float startFrequency = 0.5f;

    private const float tresholdIncreaseRate = 1.2f;

    private const float damageIncreateRate = 1.2f;

    private const float frequencyIncreaseRate = 0.95f;

    private SpaceShipPersistentData spaceShipPersistentData;

    private const string jsonFileName = "SpaceShipLevelData.json";

    public Action LevelIncreasedEvent;
    public Action DamageIncreasedEvent;
    public Action FrequencyIncreasedEvent;

    public int SpaceShipLevel
    {
        get
        {
            return spaceShipPersistentData.SpaceShipLevel;
        }
    }

    private float spaceShipXP
    {
        get
        {
            return spaceShipPersistentData.SpaceShipXP;
        }
    }

    private float spaceShipLevelThreshold
    {
        get
        {
            return spaceShipPersistentData.SpaceShipLevelThreshold;
        }
    }

    public float SpaceShipBaseDamage
    {
        get
        {
            return spaceShipPersistentData.SpaceShipBaseDamage;
        }
    }

    public float SpaceShipBaseFrequency
    {
        get
        {
            return spaceShipPersistentData.SpaceShipBaseFrequency;
        }
    }

    private void Awake()
    {
        SetInstance();
        LoadData();
    }

    private void Start()
    {
        CurrencyManager.Instance.AddCurrencyEvent += GainXP;
    }

    private void LoadData()
    {
        if(JsonDataManagement.FileExists(jsonFileName) == true)
        {
            spaceShipPersistentData = JsonDataManagement.LoadData<SpaceShipPersistentData>(fileName: jsonFileName);
        }

        else
        {
            spaceShipPersistentData = new SpaceShipPersistentData(_spaceShipLevel: startLevel,
                                                                  _spaceShipXP: startXP,
                                                                  _spaceShipLevelThreshold: startLevelThreshold,
                                                                  _spaceShipBaseDamage: startDamage,
                                                                  _spaceShipBaseFrequency: startFrequency);
        }
    }

    private void GainXP(float amount)
    {
        spaceShipPersistentData.SpaceShipXP += amount;

        IncreaseLevel();

        JsonDataManagement.SaveData<SpaceShipPersistentData>(fileName: jsonFileName, data: spaceShipPersistentData);
    }

    private void IncreaseLevel()
    {
        if(spaceShipXP > spaceShipLevelThreshold)
        {
            spaceShipPersistentData.SpaceShipLevel++;
            spaceShipPersistentData.SpaceShipLevelThreshold += spaceShipLevelThreshold * tresholdIncreaseRate;

            spaceShipPersistentData.SpaceShipBaseDamage *= damageIncreateRate;
            spaceShipPersistentData.SpaceShipBaseFrequency *= frequencyIncreaseRate;

            if (LevelIncreasedEvent != null)
                LevelIncreasedEvent.Invoke();

            if (DamageIncreasedEvent != null)
                DamageIncreasedEvent.Invoke();

            if (FrequencyIncreasedEvent != null)
                FrequencyIncreasedEvent.Invoke();
        }    
    }
}
