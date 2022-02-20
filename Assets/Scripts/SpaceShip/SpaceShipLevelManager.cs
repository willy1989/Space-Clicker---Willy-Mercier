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

    private JsonDataUser<SpaceShiplevelPersistentData> jsonDataUser;

    public Action LevelIncreasedEvent;
    public Action DamageIncreasedEvent;
    public Action FrequencyIncreasedEvent;

    public int SpaceShipLevel
    {
        get
        {
            return jsonDataUser.JsonData.SpaceShipLevel;
        }
    }

    private float spaceShipXP
    {
        get
        {
            return jsonDataUser.JsonData.SpaceShipXP;
        }
    }

    private float spaceShipLevelThreshold
    {
        get
        {
            return jsonDataUser.JsonData.SpaceShipLevelThreshold;
        }
    }

    public float SpaceShipBaseDamage
    {
        get
        {
            return jsonDataUser.JsonData.SpaceShipBaseDamage;
        }
    }

    public float SpaceShipBaseFrequency
    {
        get
        {
            return jsonDataUser.JsonData.SpaceShipBaseFrequency;
        }
    }

    private void Awake()
    {
        SetInstance();

        SpaceShiplevelPersistentData spaceShipPersistentData = new SpaceShiplevelPersistentData(_spaceShipLevel: startLevel,
                                                                                      _spaceShipXP: startXP,
                                                                                      _spaceShipLevelThreshold: startLevelThreshold,
                                                                                      _spaceShipBaseDamage: startDamage,
                                                                                      _spaceShipBaseFrequency: startFrequency);

        jsonDataUser = new JsonDataUser<SpaceShiplevelPersistentData>(_StartJsonData: spaceShipPersistentData, _jsonFileName: "spaceShipLevelData.json");
    }

    private void Start()
    {
        CurrencyManager.Instance.AddCurrencyEvent += GainXP;
    }


    private void GainXP(float amount)
    {
        jsonDataUser.JsonData.SpaceShipXP += amount;

        IncreaseLevel();

        jsonDataUser.SaveData();
    }

    private void IncreaseLevel()
    {
        if(spaceShipXP > spaceShipLevelThreshold)
        {
            jsonDataUser.JsonData.SpaceShipLevel++;
            jsonDataUser.JsonData.SpaceShipLevelThreshold += spaceShipLevelThreshold * tresholdIncreaseRate;

            jsonDataUser.JsonData.SpaceShipBaseDamage *= damageIncreateRate;
            jsonDataUser.JsonData.SpaceShipBaseFrequency *= frequencyIncreaseRate;

            if (LevelIncreasedEvent != null)
                LevelIncreasedEvent.Invoke();

            if (DamageIncreasedEvent != null)
                DamageIncreasedEvent.Invoke();

            if (FrequencyIncreasedEvent != null)
                FrequencyIncreasedEvent.Invoke();
        }    
    }
}
