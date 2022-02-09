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
            if(value < 0)
            {
                throw new ArgumentOutOfRangeException("The currency count can't be negative");
            }

            PlayerPrefs.SetFloat(Constants.CurrencyCount_PlayerPref, value);
            if(UpdateCurrencyEvent != null)
                UpdateCurrencyEvent(value);
        }
    }

    public Action<float> UpdateCurrencyEvent;
    public Action<float> AddCurrencyEvent;

    private void Awake()
    {
        SetInstance();
    }

    public void SpendCurrency(float amount)
    {
        CurrencyCount -= amount;
    }

    public void AddMoney(float amount)
    {
        CurrencyCount += amount;
        AddCurrencyEvent.Invoke(amount);
    }

    public bool HasSufficientBalance(float cost)
    {
        return (CurrencyCount - cost) >= 0;
    }
}
