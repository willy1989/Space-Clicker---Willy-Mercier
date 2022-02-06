using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipLevelManager : Singleton<SpaceShipLevelManager>
{
    private int startLevel = 1;

    private float startLevelThreshold = 30f;

    private float tresholdIncreaseRate = 1.2f;

    public Action<int> LevelIncreasedEvent;

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

    private int spaceShipLevel
    {
        get
        {
            return PlayerPrefs.GetInt(Constants.SpaceShipLevel_PlayerPref, startLevel);
        }

        set
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

    private void ResetLevelValues()
    {
        PlayerPrefs.SetFloat(Constants.SpaceShipLevel_PlayerPref, startLevel);
        PlayerPrefs.SetFloat(Constants.SpaceShipLevelTreshold_PlayerPref, startLevelThreshold);
        PlayerPrefs.SetFloat(Constants.SpaceShipXP_PlayerPref, 0f);
    }

    private void Awake()
    {
        SetInstance(); 
    }

    private void Start()
    {
        CurrencyManager.Instance.AddCurrencyEvent += GainXP;

        ResetLevelValues();
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
            spaceShipLevel++;
            spaceShipLevelThreshold += spaceShipLevelThreshold * tresholdIncreaseRate;

            if (LevelIncreasedEvent != null)
                LevelIncreasedEvent(spaceShipLevel);
        }    
    }
}
