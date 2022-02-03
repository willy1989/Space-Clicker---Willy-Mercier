using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    public float CurrencyCount
    {
        get
        {
            return PlayerPrefs.GetFloat(Constants.CurrencyCount_PlayerPref, 0);
        }

        set
        {
            // To do: throw exception when value is below 0.
            PlayerPrefs.SetFloat(Constants.CurrencyCount_PlayerPref, value);
            if(UpdateCurrencyEvent != null)
                UpdateCurrencyEvent(value);
        }
    }

    public Action<float> UpdateCurrencyEvent;

    private void Awake()
    {
        SetInstance();
    }

    private void Start()
    {
        PlayerPrefs.SetFloat(Constants.CurrencyCount_PlayerPref, 0f);
    }

    public void SpendCurrency(float ammount)
    {
        CurrencyCount -= ammount;
    }

    public void AddMoney(float ammount)
    {
        CurrencyCount += ammount;
    }

    public bool HasSufficientBalance(float ammount)
    {
        return (CurrencyCount - ammount) >= 0;
    }
}
