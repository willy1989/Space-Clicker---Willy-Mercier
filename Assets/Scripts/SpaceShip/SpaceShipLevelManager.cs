using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipLevelManager : Singleton<SpaceShipLevelManager>
{
    private int startLevel = 1;

    private float startDamage = 1;

    private float startFrequency = 0.5f;

    private float startLevelThreshold = 30f;

    private float tresholdIncreaseRate = 1.2f;

    private float damageIncreateRate = 1.2f;

    private float frequencyIncreaseRate = 0.95f;

    public Action LevelIncreasedEvent;
    public Action DamageIncreasedEvent;
    public Action FrequencyIncreasedEvent;

    private float spaceShipXP
    {
        get
        {
            return PlayerPrefs.GetFloat(Constants.SpaceShipXP_PlayerPref, 0);
        }

        set
        {
            PlayerPrefs.SetFloat(Constants.SpaceShipXP_PlayerPref, value);
        }
    }

    public int SpaceShipLevel
    {
        get
        {
            return PlayerPrefs.GetInt(Constants.SpaceShipLevel_PlayerPref, startLevel);
        }

        private set
        {
            PlayerPrefs.SetInt(Constants.SpaceShipLevel_PlayerPref, value);
        }
    }

    private float spaceShipLevelThreshold
    {
        get
        {
            return PlayerPrefs.GetFloat(Constants.SpaceShipLevelTreshold_PlayerPref, startLevelThreshold);
        }

        set
        {
            PlayerPrefs.SetFloat(Constants.SpaceShipLevelTreshold_PlayerPref, value);
        }
    }

    public float SpaceShipBaseDamage
    {
        get
        {
            return PlayerPrefs.GetFloat(Constants.SpaceShipDamageStat_PlayerPref, startDamage);
        }

        private set
        {
            PlayerPrefs.SetFloat(Constants.SpaceShipDamageStat_PlayerPref, value);
        }
    }

    public float SpaceShipBaseFrequency
    {
        get
        {
            return PlayerPrefs.GetFloat(Constants.SpaceShipFrequencyStat_PlayerPref, startFrequency);
        }

        private set
        {
            PlayerPrefs.SetFloat(Constants.SpaceShipFrequencyStat_PlayerPref, value);
        }
    }

    private void ResetLevelValues()
    {
        PlayerPrefs.SetFloat(Constants.SpaceShipLevel_PlayerPref, startLevel);
        PlayerPrefs.SetFloat(Constants.SpaceShipLevelTreshold_PlayerPref, startLevelThreshold);
        PlayerPrefs.SetFloat(Constants.SpaceShipXP_PlayerPref, 0f);
        PlayerPrefs.SetFloat(Constants.SpaceShipDamageStat_PlayerPref, startDamage);
        PlayerPrefs.SetFloat(Constants.SpaceShipFrequencyStat_PlayerPref, startFrequency);
    }

    private void Awake()
    {
        SetInstance();
    }

    private void Start()
    {
        CurrencyManager.Instance.AddCurrencyEvent += GainXP;
    }

    private void GainXP(float amount)
    {
        spaceShipXP += amount;

        IncreaseLevel();
    }

    private void IncreaseLevel()
    {
        if(spaceShipXP > spaceShipLevelThreshold)
        {
            SpaceShipLevel++;
            spaceShipLevelThreshold += spaceShipLevelThreshold * tresholdIncreaseRate;

            SpaceShipBaseDamage *= damageIncreateRate;
            SpaceShipBaseFrequency *= frequencyIncreaseRate;

            if (LevelIncreasedEvent != null)
                LevelIncreasedEvent.Invoke();

            if (DamageIncreasedEvent != null)
                DamageIncreasedEvent.Invoke();

            if (FrequencyIncreasedEvent != null)
                FrequencyIncreasedEvent.Invoke();
        }    
    }
}
